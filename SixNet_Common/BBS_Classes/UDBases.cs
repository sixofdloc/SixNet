using System;
using System.Collections.Generic;
using System.Linq;
using SixNet_BBS_Data;
using SixNet_StringUtils;
using System.IO;
using SixNet_Logger;


namespace SixNet_BBS.BBS_Classes
{
    class UDBases
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        private int Current_Area = -1;
        //private int Current_Parent_Area = -1;
        private int CurrentUDBase = -1;

        private List<IdAndKeys> Current_Area_List = null;
        private List<UDFile> Current_File_List = null;

        public UDBases(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            Current_Area = -1;
            //Current_Parent_Area = -1;
            _bbs.SendFileForTermType("udbase_entry_root", true);
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
                    _bbs.WriteLine("~l1~c7? ~c1Menu, ~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                }
                else
                {
                    _bbs.WriteLine();
                }
                if (CurrentUDBase > -1)
                {
                    _bbs.Write("~c1UDBases~c2:~c7");
                }
                else
                {
                    _bbs.Write("~c1UDBases~c2:~c7");
                }
                string command = _bbs.Input(true, false, false);
                if (command.Length > 0)
                {
                    if ("0123456789".Contains(command.Substring(0,1)))
                    {
                        if (CurrentUDBase == -1)
                        {
                            //Select item.
                            IdAndKeys selectedItem = Current_Area_List.FirstOrDefault(p => p.Keys["listid"].Equals(command));
                            if (selectedItem != null)
                            {
                                if (selectedItem.Keys["type"] == "area")
                                {
                                    _bbs.WriteLine("~l1~c7Changing to Area: " + selectedItem.Keys["title"]+"~p1");
                                    ChangeToArea(selectedItem.Id);
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~c7Changing to UDBase: " + selectedItem.Keys["title"] + "~p1");
                                    ChangeToUDBase(selectedItem.Id);
                                }
                            }
                        }
                        else
                        {
                            //We're in a udbase \
                            //CMD_DetailsByFileId(int.Parse(command));
                        }
                    }
                    else
                    {

                        switch (command.ToUpper()[0])
                        {
                            case 'H':
                                if (CurrentUDBase == -1)
                                {
                                    _bbs.SendFileForTermType("udarea_help", true);
                                }
                                else
                                {
                                    _bbs.SendFileForTermType("udbase_help", true);
                                }
                                break;
                            case '?':
                                if (CurrentUDBase == -1)
                                {
                                    _bbs.SendFileForTermType("udarea_menu", true);
                                }
                                else
                                {
                                    _bbs.SendFileForTermType("udbase_menu", true);
                                }
                                break;
                            case 'L':
                                CMD_List();
                                break;
                            case '/':
                                if (CurrentUDBase > -1)
                                {
                                    CurrentUDBase = -1;
                                    CMD_List();
                                }
                                else
                                {
                                    if (Current_Area > -1)
                                    {
                                        CurrentUDBase = -1;
                                        IdAndKeys mba = _dataInterface.UDBase_ParentArea(Current_Area);
                                        _bbs.WriteLine("~l1~c7Changing to Area: " + mba.Keys["title"] + "~p1");
                                        ChangeToArea(mba.Id);
                                    }
                                    else
                                    {
                                        _bbs.WriteLine("~l1~d2Already at top level.~d0");
                                    }
                                }
                                break;
                            case 'U':
                                if (CurrentUDBase == -1)
                                {
                                    _bbs.WriteLine("~l1~d1Select a UD base.~d0");
                                }
                                else
                                {
                                    CMD_Upload();
                                }
                                break;
                            case 'Q':
                                quitflag = true;
                                break;
                            case 'D':
                                //Download
                                if (command.Length == 1)
                                {
                                    _bbs.Write("~c1File Number~c2:~c7");
                                    int i = _bbs.InputNumber(3);
                                    if (i>-1)
                                    {
                                        CMD_Download(i.ToString());
                                    }
                                }
                                else
                                {
                                    CMD_Download(command.Substring(1, command.Length - 1));
                                }
                                break;
                        }
                    }
                }
            }
        }


        public void ChangeToArea(int areaId)
        {
            //Select Area
            Current_Area = areaId;
            if (areaId < 0)
            {
                _bbs.SendFileForTermType("udbase_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("udbase_area_" + Current_Area.ToString(), true);
            }
            Current_Area_List = _dataInterface.UDBase_List_Area(areaId, _bbs.CurrentUser.UserId);

        }

        public void ChangeToUDBase(int baseid)
        {
            //Select Area
            CurrentUDBase = baseid;
            _bbs.SendFileForTermType("udbase_entry_" + Current_Area.ToString(), true);
            //Show stats about this base
            Current_File_List = _dataInterface.ListFilesForUDBase(baseid);
        }


        public void CMD_Multi_Upload(){
        }

        public void CMD_Download(string filespec)
        {
            if (int.TryParse(filespec, out int filenum))
            {
                if (filenum <= Current_File_List.Count())
                {
                    UDFile selectedItem = Current_File_List[filenum];
                    _bbs.WriteLine("~c1Download: ~c7" + selectedItem.Filename + "~c1");
                    if (_bbs.YesNo(true, true))
                    {
                        FileTransfers ft = new FileTransfers(_bbs.State_Object);
                        string filepath = Current_Area_List.FirstOrDefault(p => p.Keys["type"].Equals("base") && p.Id.Equals(CurrentUDBase)).Keys["filepath"];
                        _bbs.WriteLine("~l1~c1Start Download Now.");
                        ft.Punter_Send(filepath + "\\" + selectedItem.Filename, (selectedItem.FileType == "P"));
                    }
                }
            }
        }
        public void CMD_Upload()
        {
            //Prompt for file name
            _bbs.Write("~l1~c1Enter a name for this file: ~c7");
            string Filename = _bbs.Input(true,false,false,false,16);
            if (Filename != "")
            {
                _bbs.Write("~l1~c1File Type(1)PRG, (2)SEQ:~c7");
                string Filetype = _bbs.Input(true, false, true, false, 1);
                if (Filetype != "")
                {
                    _bbs.Write("~l1~c1Description:~c7");
                    string Description = _bbs.Input(true, false, false, false, 38);
                    if (Description != "")
                    {
                        _bbs.WriteLine("~l1~c1Send the file now.");
                        //Punter

                        FileTransfers ft = new FileTransfers(_bbs.State_Object);
                        byte[] b = ft.Punter_Receive();
                        if (b != null)
                        {
                            _bbs.WriteLine("~l1~c1Send successful");
                            UDFile udf = new UDFile()
                            {
                                Description = Description,
                                Filename = Filename,
                                Filesize = b.Length,
                                FileType = Filetype.ToUpper(),
                                UDBaseId = CurrentUDBase,
                                Uploaded = DateTime.Now,
                                Uploader = _bbs.CurrentUser.UserId,
                                Uploadername = _bbs.CurrentUser.Username
                            };
                            string filepath = Current_Area_List.FirstOrDefault(p => p.Keys["type"].Equals("base") && p.Id.Equals(CurrentUDBase)).Keys["filepath"];
                            File.WriteAllBytes(filepath + "\\" + udf.Filename, b);
                            _dataInterface.UploadedFile(udf);
                        }
                        else
                        {
                            LoggingAPI.LogEntry("UPLOAD FAILED ->" + Filename + ", " + Description + "(" + _bbs.CurrentUser.Username + "[" + _bbs.CurrentUser.UserId.ToString() + "]" + ")");
                            _bbs.Exclaim("Send failed");
                        }
                    }
                }
            }
        }

        public void CMD_List()
        {
            if (CurrentUDBase == -1)
            {
                _bbs.WriteLine("");
                _bbs.WriteLine("");
                Current_Area_List = _dataInterface.UDBase_List_Area(Current_Area, _bbs.CurrentUser.UserId);
                foreach (IdAndKeys idak in Current_Area_List)
                {
                    if (idak.Keys["type"] == "area")
                    {
                        _bbs.WriteLine("~c7" + idak.Keys["listid"] + "~c1. ~cf" + idak.Keys["title"]+" (area)");
                    }
                    else
                    {
                        _bbs.WriteLine("~c7"+idak.Keys["listid"] + "~c1. " + idak.Keys["title"]);
                    }
                }
                _bbs.WriteLine("");
            }
            else
            {
                //List fiiles
                _bbs.Write("~s1~d2" + Utils.Center("FILES IN CURRENT BASE", _bbs.TerminalType.Columns()) + "~d0");
                //Pull a new list each time
                Current_File_List = _dataInterface.ListFilesForUDBase(CurrentUDBase);
                if (Current_File_List.Count > 0)
                {
                    foreach (UDFile tlr in Current_File_List){
                        if (tlr.Uploader == -1)
                        {
                            //skip this row
                        }
                        else
                        {
                            if (_bbs.TerminalType.Columns() == 40)
                            {
                                //                      1111111111222222222233333333334444444444555555555566666666666777777777
                                //Columns are 01234567890123456789012345678901234567890123456789012345678901234567890123456789
                                //             ID- SUBJECT---------------------------
                                //                 POSTED-----  POSTER--------------- 
                                _bbs.Write("~c7" + Utils.Clip(Current_File_List.IndexOf(tlr).ToString(), 4, true) + "~c1");
                                _bbs.WriteLine(" " + Utils.Clip(tlr.Filename, 32, true));

                                _bbs.Write(Utils.Clip("~c1Uploaded:~c3" + tlr.Uploaded.ToString("yy-MM-dd hh:mm"), 30, true));
                                _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Uploadername, 10, true));

                            }
                            else
                            {
                                //             ID- SUBJECT-------------------------------------- POSTED----- POSTER---------      
                                _bbs.Write("~c7" + Utils.Clip(Current_File_List.IndexOf(tlr).ToString(), 4, true) + "~c1");
                                _bbs.Write(" " + Utils.Clip(tlr.Filename, 40, true));
                                _bbs.Write("~c3 " + Utils.Clip(tlr.Uploaded.ToString("yy-MM-dd hh:mm"), 14, true));
                                _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Uploadername, 21, true));
                            }
                        }
                    }
                }

                else
                {
                    _bbs.WriteLine("~c7Nothing Found...~c1");
                }
                _bbs.WriteLine("~d2" + Utils.SPC(_bbs.TerminalType.Columns()) + "~d0");
            }
        }


 



    }
}
