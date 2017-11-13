using System.Collections.Generic;
using System.Linq;
using SixNet_StringUtils;
using SixNet_BBS_Data;

namespace SixNet_BBS
{
    class SysOp
    {

        private int Userid { get; set; }
        private int Columns { get; set; }

        private readonly DataInterface _dataInterface;
        private readonly BBS _bbs;

        public SysOp(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            Userid = _bbs.CurrentUser.UserId;
            Columns = _bbs.TerminalType.Columns();
        }

        public void Prompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
                _bbs.Write("~l1~c1SysOp~c2:~c7");
                string command = _bbs.Input(true, false, false);
                if (command.Length == 1)
                {

                    switch (command.Substring(0, 1).ToUpper())
                    {
                        case "H":
                            _bbs.SendFileForTermType("sysop_help", true);
                            break;
                        case "Q":
                            quitflag = true;
                            break;
                        default:
                            _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                            break;
                    }
                }
                else
                {
                    if (command.Length >= 2)
                    {
                        switch (command.Substring(0, 2).ToUpper())
                        {
                            case "LG": //List Access Groups
                                CMD_ListGroups();
                                break;
                            case "AG": //Add Access Group
                                CMD_ListGroups();
                                break;
                            case "EG": //Edit Access Group
                                CMD_ListGroups();
                                break;
                            case "RG": //Remove Access Group
                                CMD_ListGroups();
                                break;
                            default:
                                _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                                break;
                        }
                    }
                }
            }
        }


        private void CMD_AddGroup()
        {
            _bbs.Write("~l2~db" + Utils.Center("ADD ACCESS GROUP", Columns)+"~d0~c1Level(~c70-255~c1):~c7");
            string response = _bbs.Input(true, false, true);
            if (response == "") goto abortedaddgroup;
            //If level is not already in use
            return;

            abortedaddgroup:
            _bbs.WriteLine("~l1~d2Aborted Add Access Group.~d0~c1");
        }

        private void CMD_ListGroups()
        {
            List<AccessGroup> aglist = _dataInterface.ListAccessGroups().OrderBy(p => p.AccessGroupNumber).ToList();
            _bbs.Write("~l2~db" + Utils.Center("ACCESS GROUPS", Columns));
            _bbs.Write("LEVEL CD  MC  RM SY TITLE               ~d0");
            foreach (AccessGroup ag in aglist)
            {
                _bbs.WriteLine(
                    "~c1" + Utils.Clip(ag.AccessGroupNumber.ToString(), 5, true)
                   + " ~c7" + Utils.Clip(ag.CallsPerDay.ToString(), 3, true)
                   + " ~c8" + Utils.Clip(ag.MinutesPerCall.ToString(), 3, true)
                   + " ~c7" + (ag.Flag_Remote_Maintenance ? "1 " : "0 ")
                   + " ~c8" + (ag.Is_SysOp ? "1 " : "0 ")
                   + " ~c7" + Utils.Clip(ag.Title, 19, true)
                   );
            }
            _bbs.WriteLine("~db" + Utils.SPC(Columns) + "~d0~c1~l1");
        }


    }
}
