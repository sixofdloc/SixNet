using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Net_Data;
using Net_Data.Classes;
using Net_Data.Enums;
using Net_Data.Models;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class PFiles
    {

        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        private PFileArea _currentPFileArea ;
        private List<AreaListRow> _currentList ;

        private bool _quitFlag;

        public PFiles(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            _bbs.currentArea = "PFiles";
            _currentPFileArea = null;
            _bbs.SendFileForTermType("pfile_entry_root", false);
            CmdList();
        }

        public void Prompt()
        {
            _quitFlag = false;
            while ((!_quitFlag) && _bbs.Connected)
            {
                //Show GFiles Prompt
                if (!_bbs.expertMode)
                {
                    _bbs.WriteLine("~l1~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                }
                else
                {
                    _bbs.WriteLine();
                }
                _bbs.Write("~c1PFiles~c2:~c7");
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
                                CmdRun(selectedItem.Id);
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
                    _bbs.SendFileForTermType("pfile_help", true);
                    break;
                case 'L':
                    CmdList();
                    break;
                case '/':
                    if (_currentPFileArea != null)
                    {
                        CmdAreaChange(_currentPFileArea.ParentAreaId);
                    }
                    break;
                case 'Q':
                    _quitFlag = true;
                    break;
                case 'T':
                    CmdRun("PFiles/Empire.dll");
                    break;
                default:
                    if (_bbs.sysopIdentified)
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
                        _bbs.SendFileForTermType("sysop_pfile_help", true);
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
                _currentPFileArea = null;
            }
            else
            {
                _currentPFileArea = _bbsDataCore.GetPFileArea((int)areaId);
            }
            if (areaId == null)
            {
                _bbs.SendFileForTermType("pfile_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("pfile_entry_" + _currentPFileArea.Id.ToString(), true);
            }
            CmdList();

        }

        public void CmdList()
        {
            _bbs.WriteLine();
            _bbs.Write("~d7" + Utils.Center("PFILES IN THIS AREA", _bbs.terminalType.Columns()) + "~d0");
            _currentList = _bbsDataCore.PFileListArea(_currentPFileArea?.Id, _bbs.currentUser.Id);
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

        private void CmdRun(int pfileId)
        {

                var pFileDetail = _bbsDataCore.GetPFileDetail(pfileId);
            if (pFileDetail != null)
            {
                CmdRun(pFileDetail.FilePath + pFileDetail.Filename);
            }
        }

        private void CmdRun(string dllname)
        {
            Assembly assembly = Assembly.LoadFrom(dllname);
            MethodInfo method = null;
            Type pfiletype = null;
            int i = 0;
            while ((method == null) && i < assembly.GetTypes().Count())
            {
                Type t = assembly.GetTypes()[i];
                pfiletype = t;
                method = t.GetMethod("Main");
                i++;
            }
            Object[] param = new Object[] { _bbs, _bbsDataCore };
            var instance = Activator.CreateInstance(pfiletype, param);
            method.Invoke(instance, new object[] { });
        }



        //private PFileArea currentPFileArea;

        //private List<IdAndKeys> Current_Area_List = null;

        //public PFiles(BBS bbs, BBSDataCore bbsDataCore)
        //{
        //    _bbs = bbs;
        //    _bbsDataCore = bbsDataCore;
        //    currentPFileArea = null;
        //    _bbs.SendFileForTermType("pfile_entry_root", false);
        //    CMD_List();
        //}

        //public void Prompt()
        //{
        //    bool quitflag = false;
        //    while ((!quitflag) && _bbs.Connected)
        //    {
        //        //Show Main Prompt
        //        _bbs.WriteLine("~l1~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
        //        _bbs.Write("~c1PFiles~c2:~c7");
        //        string command = _bbs.Input(true, false, false);
        //        if (command.Length > 0)
        //        {
        //            if ("0123456789".Contains(command.Substring(0, 1)))
        //            {
        //                //Select item.
        //                IdAndKeys selectedItem = Current_Area_List.FirstOrDefault(p => p.Keys["listid"].Equals(command));
        //                if (selectedItem != null)
        //                {
        //                    if (selectedItem.Keys["type"] == "area")
        //                    {
        //                        ChangeToArea(selectedItem.Id);
        //                    }
        //                    else
        //                    {
        //                        //Run this file 
        //                        RunPFile(selectedItem.Keys["title"]);

        //                    }
        //                }
        //            }
        //            else
        //            {

        //                switch (command.ToUpper()[0])
        //                {
        //                    case 'H':
        //                        _bbs.SendFileForTermType("pfile_help", true);
        //                        break;
        //                    case 'L':
        //                        CMD_List();
        //                        break;
        //                    case '/':
        //                        if (currentPFileArea != null && currentPFileArea.ParentAreaId != null)
        //                        {
        //                            ChangeToArea(currentPFileArea.ParentAreaId);
        //                        }
        //                        break;
        //                    case 'T':
        //                        RunPFile("PFiles/Empire.dll");
        //                        break;
        //                    case 'Q':
        //                        quitflag = true;
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //}


        //public void ChangeToArea(int areaId)
        //{
        //    //Select Area
        //    currentPFileArea = _bbsDataCore.GetPFileArea(areaId);
        //    if (areaId > 0)
        //    {
        //        _bbs.SendFileForTermType("pfile_entry_root", true);
        //    }
        //    else
        //    {
        //        _bbs.SendFileForTermType("pfile_entry_" + Current_Pfile_Area.ToString(), true);
        //    }
        //    CMD_List();

        //}

        //public void CMD_List()
        //{
        //    _bbs.WriteLine("");
        //    _bbs.WriteLine("");
        //    Current_Area_List = _bbsDataCore.PFile_List_Area(Current_Pfile_Area);
        //    foreach (IdAndKeys idak in Current_Area_List)
        //    {
        //        _bbs.WriteLine(idak.Keys["listid"] + ". " + idak.Keys["desc"]);
        //    }
        //    _bbs.WriteLine("");
        //}


    }
}
