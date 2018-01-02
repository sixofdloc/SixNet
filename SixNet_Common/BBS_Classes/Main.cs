using System;
using SixNet_BBS.BBS_Classes.Editors;
using SixNet_BBS_Data;
using SixNet_StringUtils;
using SixNet_Logger;

namespace SixNet_BBS.BBS_Classes
{
    class Main
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;
        private readonly BBSConfig _bbsConfig;

        public Main(BBS bbs, DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            _bbsConfig = _dataInterface.GetBBSConfig();
        }

        public void MainPrompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                if (!_bbs.DND_Override) _bbs.DoNotDisturb = false;
                //Show Main Prompt
                if (!_bbs.ExpertMode)
                {
                    if (_bbs.TerminalType.Columns() == 40)
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
                _bbs.CurrentArea = "Main Prompt";
                _bbs.Write("~c1Main~c2:~c7");
                string command = _bbs.Input(true, false, false,true,10);
                if (command.Length > 0)
                {
                    switch (command.ToString().ToUpper())
                    {
                        case "?":
                            _bbs.SendFileForTermType("MainMenu", true);
                            break;
                        case "B":
                            MessageBases mb = new MessageBases(_bbs, _dataInterface);
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
                                GFiles gf = new GFiles(_bbs, _dataInterface);
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
                                PFiles pf = new PFiles(_bbs, _dataInterface);
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
                                _bbs.Gw.AddLine(_bbs.CurrentUser.UserId);
                            }
                            quitflag = true;
                            break;
                        case "SY":
                            if (_dataInterface.GetAccessGroup(_bbs.CurrentUser.AccessLevel).Is_SysOp)
                            {
                                _bbs.Write("~l1~c1Password:~c7");
                                string sy = _bbs.Input(true, true, false);
                                if (sy.ToUpper() == _bbsConfig.SysopMenuPass.ToUpper())
                                {
                                    _bbs.Sysop_Identified = true;
                                    SysOp sys = new SysOp(_bbs, _dataInterface);
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
                            UDBases ud = new UDBases(_bbs, _dataInterface);
                            ud.Prompt();
                            //break;
                            //UDBases ud = new UDBases(bbs);
                            //ud.Prompt();
                            break;

                            
                        case "WHO":
                            CMD_Who();
                            break;
                        case "X":
                            _bbs.ExpertMode = !_bbs.ExpertMode;
                            _bbs.WriteLine("~l1~c1Expert mode is ~c7" + (_bbs.ExpertMode ? "ON" : "OFF") + "~c1.");
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

                                if (_bbs.Sysop_Identified)
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
                if ((!quitflag) && (!_bbs.DoNotDisturb))
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

            } else {
                _bbs.WriteLine("~l1~d2Unknown command.~d0~c1");
            }

        }

        private void CMD_Who()
        {
            int columns = _bbs.TerminalType.Columns();
            _bbs.Write("~l2~d9" + Utils.Center("USERS CURRENTLY ONLINE", columns));
            _bbs.Write(Utils.Clip("USERID HANDLE", columns, true) + "~d0");

            foreach (BBS bbs2 in _bbs.Host_System.GetAllNodes())
            {
                _bbs.Write("~c1" + Utils.Clip(bbs2.CurrentUser.UserId.ToString(), 7, true));
                _bbs.Write("~c7" + Utils.Clip(bbs2.CurrentUser.Username, 33, true));
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
                foreach (BBS bbs2 in _bbs.Host_System.GetAllNodes())
                {
                   
                    if (bbs2.CurrentUser.UserId.Equals(userid))
                    {
                        if (bbs2.DoNotDisturb)
                        {
                            _bbs.WriteLine("~l1~d2That user is currently in DND mode.~d0");
                        }
                        else
                        {
                            bbs2.MessageQueue.Add("~l1~c4OLM>~c7" + _bbs.CurrentUser.Username + "~c2:~c1" + message);
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
            if (_bbs.IsSlackEnabled())
            {
                try
                {
                    //What userid and what message?
                    int messagebegins = command.IndexOf(' ');
                    string message = command.Substring(messagebegins, command.Length - messagebegins);
                    _bbs.SlackLogMessage(message);
                }
                catch (Exception e)
                {
                    LoggingAPI.Error(e);
                }
            } else
            {
                _bbs.WriteLine("~l1~c2Slack integration not enabled~c1.");
            }
        }

        private void CMD_DND()
        {
            _bbs.DoNotDisturb = !_bbs.DoNotDisturb;
            _bbs.DND_Override = _bbs.DoNotDisturb; //lets the system know we set it, not auto
            _bbs.WriteLine("~l1~c2DND is now ~c7" + (_bbs.DoNotDisturb ? "ON" : "OFF") + "~c1.");

        }
        private void CMD_Feedback()
        {
            _bbs.Write("~l1~c1Leave feedback?");
            if (_bbs.YesNo(true, true))
            {
                Line_Editor av = new Line_Editor(_bbs);
                if (av.Edit(null))
                {
                    _dataInterface.NewFeedback("Feedback", av.GetMessage(), _bbs.CurrentUser.UserId);
                }
            }
        }
    }
}
