using System.Collections.Generic;
using System.Linq;
using System.IO;
using SixNet_BBS_Data;
using SixNet_StringUtils;

namespace SixNet_BBS
{
    class GFiles
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        private int Current_Gfile_Area = -1;
        private int Current_Parent_Area;

        private List<IdAndKeys> Current_Area_List = null;

        public GFiles(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            _bbs.CurrentArea = "GFiles";
            Current_Gfile_Area = -1;
            Current_Parent_Area = -1;
            _bbs.SendFileForTermType("gfile_entry_root", false);
            CMD_List();
        }

        public void Prompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
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
                    if ("0123456789".Contains(command.Substring(0,1)))
                    {
                        //Select item.
                        IdAndKeys selectedItem = Current_Area_List.FirstOrDefault(p => p.Keys["listid"].Equals(command));
                        if (selectedItem != null)
                        {
                            if (selectedItem.Keys["type"] == "area")
                            {
                                ChangeToArea(selectedItem.Id);
                            }
                            else
                            {
                                //Show this file
                                CMD_Read(int.Parse(selectedItem.Keys["gfiledetailid"]));
                            }
                        }
                    }
                    else
                    {

                        switch (command.ToUpper()[0])
                        {
                            case 'H':
                                _bbs.SendFileForTermType("gfile_help", true);
                                break;
                            case 'L':
                                CMD_List();
                                break;
                            case '/':
                                if (Current_Gfile_Area > -1)
                                {
                                    ChangeToArea(_dataInterface.GFile_ParentArea(Current_Gfile_Area));
                                }
                                break;
                            case 'Q':
                                quitflag = true;
                                break;
                            default:
                                if (_bbs.Sysop_Identified)
                                {
                                    CMD_SysOp(command);
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                                }
                                break;

                        }
                    }
                }
            }
        }

        private void CMD_SysOp(string command)
        {
            if (command.Length >= 2)
            {
                switch (command.Substring(0, 2).ToUpper())
                {
                    case "??":
                        _bbs.SendFileForTermType("Sysop_Gfile_Help", true);
                        break;
                    case "IF":
                        //Import Files to current area
                        CMD_SysOp_Import_Files();
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
        public void ChangeToArea(int areaId)
        {
            //Select Area
            Current_Gfile_Area = areaId;
            if (areaId > 0)
            {
                _bbs.SendFileForTermType("gfile_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("gfile_entry_" + Current_Gfile_Area.ToString(), true);
            }
            CMD_List();

        }

        public void CMD_List()
        {
            _bbs.Write("~s1~d7" + Utils.Center("GFILES IN THIS AREA", _bbs.TerminalType.Columns()) + "~d0");
            Current_Area_List = _dataInterface.GFile_List_Area(Current_Gfile_Area,_bbs.CurrentUser.UserId);
            int row = 1;
            foreach (IdAndKeys idak in Current_Area_List)
            {
                _bbs.WriteLine("~c1"+Utils.Clip(idak.Keys["listid"],4,true) + " ~c8" + Utils.Clip(idak.Keys["desc"],33,true));
                row++;
                if (row == 23)
                {
                    _bbs.Write("~c7M~c1ore,~c7A~c1bort:~c1");
                    char c = _bbs.GetChar();
                    if (c.ToString().ToUpper() == "A"){
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

        public void CMD_Read(int fileId)
        {
            GFileDetail gfile = _dataInterface.GetGFileDetail(fileId);
            _bbs.WriteLine("~l2~c1Filename: ~c7" + gfile.DisplayFilename);
            _bbs.WriteLine("~c1Description: ~c7" + gfile.Description);
            _bbs.Write("~c7"+Utils.Repeat('\xc0',_bbs.TerminalType.Columns())+"~c1");
            outputrow = 0;
            string[] lines = File.ReadAllLines(gfile.Filename);
            foreach (string s in lines)
            {
                    if (_bbs.TerminalType.Columns()==40)
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
                        if (!OutputLine(s,true)) return;
                    }
            }
        }
        int outputrow;
        private bool OutputLine(string s, bool cr )
        {
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
                _bbs.Write("~c7M~c1ore,~c7A~c1bort:~c1");
                char c = _bbs.GetChar();
                if (c.ToString().ToUpper() == "A")
                {
                    _bbs.WriteLine("Abort");
                    return false;
                }
                if (c.ToString().ToUpper() == "M")
                {
                    _bbs.WriteLine("More");
                    outputrow = 0;
                    return true;
                }

            }
    return true;
        }

        public void CMD_SysOp_Import_Files()
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
                string[] lines = File.ReadAllLines(path+"\\import.txt");
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
                    if (_dataInterface.AddGFile(Current_Gfile_Area, shortname,filename, description,petscii))
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
