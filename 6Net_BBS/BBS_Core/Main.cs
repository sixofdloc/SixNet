using System;
using System.Linq;
using Net_BBS.BBS_Core.Editors;
using Net_Data;
using Net_Data.Models;
using Net_Logger;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class Main
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;
        private readonly BBSConfig _bbsConfig;

        public Main(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            _bbsConfig = bbsDataCore.GetBBSConfig();
        }

        public void MainPrompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                if (!_bbs.overrideDoNotDisturb) _bbs.doNotDisturb = false;
                //Show Main Prompt
                if (!_bbs.expertMode)
                {
                    if (_bbs.terminalType.Columns() == 40)
                    {
                        _bbs.WriteLine("~l2~c7? ~c1Menu~c2,~c7H~c1elp~c2,~c7B~c1ases~c2,~c7G~c1Files~c2,~c7P~c1Files~c2,~c7Q~c1uit");
                    }
                    else
                    {
                        _bbs.WriteLine("~l2~c7? ~c1Help~c2, ~c7B~c1ases~c2, ~c7U~c1DBases~c2, ~c7G~c1Files~c2, ~c7P~c1Files~c2, ~c7Q~c1uit");
                    }
                }
                else
                {
                    _bbs.WriteLine();
                }
                _bbs.currentArea = "Main Prompt";
                _bbs.Write("~c1Main~c2:~c7");
                string command = _bbs.Input(true, false, false, true, 10);
                if (command.Length > 0)
                {
                    switch (command.ToString().ToUpper())
                    {
                        case "?":
                            _bbs.SendFileForTermType("MainMenu", true);
                            break;
                        case "B":
                            MessageBases mb = new MessageBases(_bbs, _bbsDataCore);
                            mb.Prompt();
                            break;
                        case "DND":
                            CMD_DND();
                            break;
                        case "F":
                            CMD_Feedback();
                            break;
                        case "G":
                            try
                            {
                                GFiles gf = new GFiles(_bbs, _bbsDataCore);
                                gf.Prompt();
                            }
                            catch (Exception e)
                            {
                                LoggingAPI.LogEntry("Exception in Main.MainPrompt: " + e);
                                //Log this?
                            }
                            break;
                        case "P":
                            try
                            {
                                PFiles pf = new PFiles(_bbs, _bbsDataCore);
                                pf.Prompt();
                            }
                            catch (Exception e)
                            {
                                LoggingAPI.LogEntry("Exception in Main.MainPrompt: " + e);
                                //Log this?
                            }
                            break;
                        case "Q!":
                            quitflag = true;
                            break;
                        case "Q":
                            CMD_Feedback();
                            _bbs.Write("~l1~c1Leave one-liner?");
                            if (_bbs.YesNo(true, true))
                            {
                                var graffitiWall = new GraffitiWall(_bbs, _bbsDataCore);
                                graffitiWall.AddLine(_bbs.currentUser.Id);
                            }
                            quitflag = true;
                            break;
                        case "SY":
                            //If the current user has any groups that would allow sysop access
                            if (_bbs.currentUser.UserAccessGroups.Any(p=>p.AccessGroup.AllowSysOp) )
                            {
                                _bbs.Write("~l1~c1Password:~c7");
                                string sy = _bbs.Input(true, true, false);
                                if (sy.ToUpper() == _bbsConfig.SysOpMenuPassword.ToUpper())
                                {
                                    _bbs.sysopIdentified = true;
                                    SysOp sys = new SysOp(_bbs, _bbsDataCore);
                                    sys.Prompt();
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~d2Invalid password.  Fuck off.~d0~c1");
                                }
                            }
                            else
                            {
                                _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                            }
                            break;
                        case "UD":
                        case "U":
                            UDBases ud = new UDBases(_bbs, _bbsDataCore);
                            ud.Prompt();
                            //break;
                            //UDBases ud = new UDBases(bbs);
                            //ud.Prompt();
                            break;


                        case "WHO":
                            CMD_Who();
                            break;
                        case "X":
                            _bbs.expertMode = !_bbs.expertMode;
                            _bbs.WriteLine("~l1~c1Expert mode is ~c7" + (_bbs.expertMode ? "ON" : "OFF") + "~c1.");
                            break;
                        default:
                            //Test multi-part commands
                            if (command.Length > 3 && command.Substring(0, 3).ToUpper() == "OLM")
                            {
                                CMD_OLM(command);
                            }
                            else if (command.Length > 3 && command.Substring(0, 3).ToUpper() == "SLM")
                            {
                                CMD_SLM(command);
                            }
                            else
                            {

                                if (_bbs.sysopIdentified)
                                {
                                    CMD_SysOp(command);
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
                                }
                            }
                            break;
                    }
                }
                if ((!quitflag) && (!_bbs.doNotDisturb))
                {
                    //Show any received OLMs
                    _bbs.FlushOLMQueue();
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
                        _bbs.SendFileForTermType("Sysop_MainMenu", true);
                        break;
                    case "AL":
                        //Add Access Level
                        break;
                    case "EL":
                        //Edit Access Level
                        break;
                    case "RF":
                        //Read Feedback
                        break;
                    case "NU":
                        //New User Validation
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

        private void CMD_Who()
        {
            int columns = _bbs.terminalType.Columns();
            _bbs.Write("~l2~d9" + Utils.Center("USERS CURRENTLY ONLINE", columns));
            _bbs.Write(Utils.Clip("USERID HANDLE", columns, true) + "~d0");

            foreach (BBS bbs2 in _bbs._bbsHost.GetAllNodes())
            {
                _bbs.Write("~c1" + Utils.Clip(bbs2.currentUser.Id.ToString(), 7, true));
                _bbs.Write("~c7" + Utils.Clip(bbs2.currentUser.Username, 33, true));
            }
            _bbs.WriteLine("~d9" + Utils.SPC(columns) + "~d0~c1");

        }

        private void CMD_OLM(string command)
        {
            try
            {
                //What userid and what message?
                int messagebegins = command.IndexOf(' ', 4);
                string user = command.Substring(3, messagebegins - 3);
                int userid = int.Parse(user);
                string message = command.Substring(messagebegins, command.Length - messagebegins);
                foreach (BBS bbs2 in _bbs._bbsHost.GetAllNodes())
                {

                    if (bbs2.currentUser.Id.Equals(userid))
                    {
                        if (bbs2.doNotDisturb)
                        {
                            _bbs.WriteLine("~l1~d2That user is currently in DND mode.~d0");
                        }
                        else
                        {
                            bbs2.messageQueue.Add("~l1~c4OLM>~c7" + _bbs.currentUser.Username + "~c2:~c1" + message);
                            _bbs.WriteLine("~l1~c9Your message was delivered.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Main.CMD_OLM: " + e);
            }
        }

        private void CMD_SLM(string command)
        {
            //if (_bbs.IsSlackEnabled())
            //{
            //    try
            //    {
            //        //What userid and what message?
            //        int messagebegins = command.IndexOf(' ');
            //        string message = command.Substring(messagebegins, command.Length - messagebegins);
            //        _bbs.SlackLogMessage(message);
            //    }
            //    catch (Exception e)
            //    {
            //        LoggingAPI.Error(e);
            //    }
            //}
            //else
            //{
            //    _bbs.WriteLine("~l1~c2Slack integration not enabled~c1.");
            //}
        }

        private void CMD_DND()
        {
            _bbs.doNotDisturb = !_bbs.doNotDisturb;
            _bbs.overrideDoNotDisturb = _bbs.doNotDisturb; //lets the system know we set it, not auto
            _bbs.WriteLine("~l1~c2DND is now ~c7" + (_bbs.doNotDisturb ? "ON" : "OFF") + "~c1.");

        }
        private void CMD_Feedback()
        {
            _bbs.Write("~l1~c1Leave feedback?");
            if (_bbs.YesNo(true, true))
            {
                Line_Editor av = new Line_Editor(_bbs);
                if (av.Edit(null))
                {
                    _bbsDataCore.NewFeedback("Feedback", av.GetMessage(), _bbs.currentUser.Id);
                }
            }
        }
    }
}
