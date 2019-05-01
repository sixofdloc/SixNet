using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Net_Data;
using Net_Data.Classes;
using Net_Data.Enums;
using Net_Data.Models;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class GFiles
    {

        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        private GFileArea _currentGFileArea = null;
        private List<AreaListRow> _currentList = null;

        private bool _quitFlag = false;

        public GFiles(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            _bbs.CurrentArea = "GFiles";
            _currentGFileArea = null;
            _bbs.SendFileForTermType("gfile_entry_root", false);
            CmdList();
        }

        public void Prompt()
        {
            _quitFlag = false;
            while ((!_quitFlag) && _bbs.Connected)
            {
                //Show GFiles Prompt
                if (!_bbs.ExpertMode)
                {
                    _bbs.WriteLine("~l1~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                }
                else
                {
                    _bbs.WriteLine();
                }
                _bbs.Write("~c1GFiles~c2:~c7");
                string command = _bbs.Input(true, false, false);
                if (command.Length > 0)
                {
                    if ("0123456789".Contains(command.Substring(0, 1)))
                    {
                        //Select item.
                        AreaListRow selectedItem = _currentList.FirstOrDefault(p => p.ListId.ToString().Equals(command));
                        if (selectedItem != null)
                        {
                            if (selectedItem.RowType == AreaListRowType.Area)
                            {
                                CmdAreaChange(selectedItem.Id);
                            }
                            else
                            {
                                //Show this file
                                CmdRead(selectedItem.Id);
                            }
                        }
                    }
                    else
                    {
                        HandleCommand(command);
                    }
                }
            }
        }

        private void HandleCommand(string command)
        {
            _bbs.WriteLine();
            switch (command.ToUpper()[0])
            {
                case 'H':
                    _bbs.SendFileForTermType("gfile_help", true);
                    break;
                case 'L':
                    CmdList();
                    break;
                case '/':
                    if (_currentGFileArea != null)
                    {
                        CmdAreaChange(_currentGFileArea.ParentAreaId);
                    }
                    break;
                case 'Q':
                    _quitFlag = true;
                    break;
                default:
                    if (_bbs.Sysop_Identified)
                    {
                        HandleSysOpCommand(command);
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                    }
                    break;
            }
        }

        private void HandleSysOpCommand(string command)
        {
            if (command.Length >= 2)
            {
                switch (command.Substring(0, 2).ToUpper())
                {
                    case "??":
                        _bbs.SendFileForTermType("sysop_gfile_help", true);
                        break;
                    case "IF":
                        //Import Files to current area
                        CmdImportFiles();
                        break;
                    default:
                        _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                        break;
                }
            }
            else
            {
                _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
            }

        }
        public void CmdAreaChange(int? areaId)
        {
            //Select Area
            if (areaId == null)
            {
                _currentGFileArea = null;
            }
            else
            {
                _currentGFileArea = _bbsDataCore.GetGFileArea((int)areaId);
            }
            if (areaId == null)
            {
                _bbs.SendFileForTermType("gfile_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("gfile_entry_" + _currentGFileArea.Id.ToString(), true);
            }
            CmdList();

        }

        public void CmdList()
        {
            _bbs.WriteLine();
            _bbs.Write("~d7" + Utils.Center("GFILES IN THIS AREA", _bbs.TerminalType.Columns()) + "~d0");
            _currentList = _bbsDataCore.GFileListArea(_currentGFileArea?.Id, _bbs.CurrentUser.Id);
            int row = 1;
            foreach (AreaListRow areaListRow in _currentList)
            {
                if (areaListRow.RowType == AreaListRowType.Area)
                {
                    _bbs.WriteLine("~c1" + Utils.Clip(areaListRow.ListId.ToString(), 3, true) + " ~c8" + Utils.Clip(areaListRow.Title, 33, true));
                }
                else
                {
                    _bbs.WriteLine("~c1" + Utils.Clip(areaListRow.ListId.ToString(), 3, true) + " ~c8" + Utils.Clip(areaListRow.Description, 33, true));
                }
                row++;
                if (row == 23)
                {
                    _bbs.Write("~c7M~c1ore,~c7A~c1bort:~c1");
                    char c = _bbs.GetChar();
                    if (c.ToString().ToUpper() == "A")
                    {
                        _bbs.WriteLine("Abort");
                        break;
                    }
                    if (c.ToString().ToUpper() == "M")
                    {
                        _bbs.WriteLine("More");
                        row = 0;
                    }
                }
            }
            _bbs.WriteLine("");
        }

        public void CmdRead(int fileId)
        {
            GFileDetail gfile = _bbsDataCore.GetGFileDetail(fileId);
            _bbs.WriteLine("~l2~c1Filename: ~c7" + gfile.Title);
            _bbs.WriteLine("~c1Description: ~c7" + gfile.Description);
            _bbs.Write("~c7" + Utils.Repeat('\xc0', _bbs.TerminalType.Columns()) + "~c1");
            outputrow = 0;
            string[] lines = File.ReadAllLines(gfile.FilePath + gfile.Filename);
            foreach (string s in lines)
            {
                if (_bbs.TerminalType.Columns() == 40)
                {
                    if (s.Length > 40)
                    {
                        List<string> t = Utils.Split(s, 40).ToList();
                        foreach (string tt in t)
                        {
                            if (t[t.Count - 1] == tt)
                            {
                                if (!OutputLine(tt, true)) return;
                            }
                            else
                            {
                                if (!OutputLine(tt, false)) return;
                            }
                        }
                    }
                    else
                    {
                        if (!OutputLine(s, true)) return;
                    }
                }
                else
                {
                    if (!OutputLine(s, true)) return;
                }
            }
        }
        int outputrow;
        private bool OutputLine(string s, bool cr)
        {
            var result = true;
            if (cr)
            {
                _bbs.WriteLine(s);
            }
            else
            {
                _bbs.Write(s);
            }
            outputrow++;
            if (outputrow == 23)
            {
                result = _bbs.MoreOrAbort();
                if (result) outputrow = 0;
            }
            return result;
        }

        public void CmdImportFiles()
        {
            //Prompt for path
            _bbs.Write("~l2~c1Import Files~l1Path:~c7");
            string path = _bbs.Input(true, false, false);
            _bbs.Write("~l1~c7P~c1ETSCII or ~c7A~c1SCII:~c7");
            bool petscii = false;
            char PA = _bbs.GetChar();
            if (PA.ToString().ToUpper() == "P")
            {
                petscii = true;
                _bbs.WriteLine("PETSCII~c1");
            }
            else
            {
                _bbs.WriteLine("ASCII~c1");
            }
            //Open import.txt at that path
            if (File.Exists(path + "\\import.txt"))
            {
                //Show proposed import
                string[] lines = File.ReadAllLines(path + "\\import.txt");
                //Confirm
                //For each Row, create a GFileDetail entry.
                _bbs.WriteLine("~l1~c1Importing...~l1");
                List<string> badlines = new List<string>();
                foreach (string s in lines)
                {
                    int firstspace = s.IndexOf(' ');
                    string shortname = s.Substring(0, firstspace);
                    string filename = path + "\\" + shortname;
                    string description = s.Substring(firstspace + 1, s.Length - (firstspace + 1));
                    _bbs.Write("~c1Adding " + shortname + "...");
                    if (_bbsDataCore.AddGFileDetail(_currentGFileArea.Id, path, shortname, filename, description, "", petscii))
                    {
                        _bbs.WriteLine("~c5GOOD.");
                    }
                    else
                    {
                        badlines.Add(s);
                        _bbs.WriteLine("~c2BAD.");
                    }

                }
                if (badlines.Count > 0)
                {
                    File.WriteAllLines(path + "\\badlines.txt", badlines);
                }
            }
            else
            {
                _bbs.WriteLine("~l1~c2No ~caimport.txt~c2 found!~c1~g1");
            }

        }

    }
}
