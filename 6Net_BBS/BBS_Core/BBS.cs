using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Net_BBS.Interfaces;
using Net_Comm.Classes;
using Net_Comm.TermTypes;
using Net_Data;
using Net_Data.Models;
using Net_Logger;

namespace Net_BBS.BBS_Core
{
    public class BBS : Communicator
    {
        public DateTime connectionTimestamp;
        //Exposed for server to display/act on
        public User currentUser { get; set; }
        public bool sysopIdentified { get; set; }
        public string currentArea { get; set; } //used for upper display

        public bool expertMode;

        public bool doNotDisturb;
        public bool overrideDoNotDisturb;

        public List<string> messageQueue = new List<string>();
        public System.Timers.Timer messageQueueTimer = new System.Timers.Timer(1000);

        //Nuts & Bolts
        public readonly IBBSHost _bbsHost;
        private readonly BBSDataCore _bbsDataCore;
        public readonly string _remoteAddress = "0.0.0.0";
        public GraffitiWall graffitiWall { get; set; }


        //private readonly bool _slackEnabled;
        //private readonly SlackIntegration _slackIntegration;

        public BBS(IBBSHost bbsHost, StateObject stateObject, string ConnectionString)
        {
            connectionTimestamp = DateTime.Now;
            _bbsDataCore = new BBSDataCore(ConnectionString);
            _bbsHost = bbsHost;
            _stateObject = stateObject;
            messageQueueTimer.Enabled = false;
            messageQueueTimer.Interval = 100;
            messageQueueTimer.Elapsed += MessageQueueTimer_Elapsed;
            _remoteAddress = stateObject.RemoteAddress;
            //_slackEnabled = (_dataInterface.GetUserDefinedField(0, "SLACKENABLED") == "1");
            //if (_slackEnabled) _slackIntegration = new SlackIntegration(_dataInterface);

            LoggingAPI.Information(_remoteAddress + ": User Connected");
        }

        void MessageQueueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!doNotDisturb)
            {
                if (!overrideDoNotDisturb)
                {
                    messageQueueTimer.Enabled = false;
                    FlushOLMQueue();
                    messageQueueTimer.Enabled = true;
                }

            }
        }

        public void FlushOLMQueue()
        {
            bool b = messageQueueTimer.Enabled;
            if (b) messageQueueTimer.Enabled = false;
            List<string> all = messageQueue.Where(p => true).ToList();
            foreach (string s in all)
            {
                WriteLine(s);
                messageQueue.Remove(s);
            }
            messageQueueTimer.Enabled = b;
        }

        public bool Go()
        {
            if (_stateObject != null)
            {
                doNotDisturb = true;
                overrideDoNotDisturb = false;
                messageQueue = new List<string>();
                _stateObject.AddDisconnectHandler(Disconnect);
                _stateObject.AddReceiver(Receive);
                currentChar = 0x00;
                try
                {
                    sysopIdentified = false;
                    //Terminal Detection
                    terminalType = new TermType_Default();
                    TermDetect td = new TermDetect(this);
                    terminalType = td.Detect();
                    LoggingAPI.Information(_remoteAddress + ": " + terminalType.TerminalTypeName() + " terminal was detected");
                    Write("~s1");
                    //Welcome Screen
                    SendFileForTermType("Welcome", false);
                    if (terminalType.C64_Color())
                    {
                        AnyKey(true, false);
                        Write("~s2");
                    }
                    else
                    {
                        AnyKey(true, true);
                    }

                    //Login
                    Login li = new Login(this, _bbsDataCore);
                    currentUser = li.LogIn();
                    if (currentUser != null)
                    {
                        //SlackLogMessage(CurrentUser.Username + " logged on.");
                        LoggingAPI.Information(_remoteAddress + ": " + currentUser.Username + "(" + currentUser.Id.ToString() + ")" + " logged in.");
                        int CallLogId = _bbsDataCore.RecordConnection(currentUser.Id);

                        var lastTen = new LastTen(this, _bbsDataCore);
                        lastTen.ShowLastTenCalls();
                        AnyKey(true, false);

                        graffitiWall = new GraffitiWall(this, _bbsDataCore);
                        graffitiWall.DisplayWall();
                        AnyKey(true, false);

                        News ne = new News(this, _bbsDataCore);
                        ne.DisplayNews();
                        AnyKey(true, false);

                        Main main = new Main(this, _bbsDataCore);
                        try
                        {
                            messageQueueTimer.Enabled = true;
                            Thread.Sleep(100);
                            main.MainPrompt();
                            messageQueueTimer.Enabled = false;
                            Thread.Sleep(100);
                        }
                        catch (Exception exception)
                        {
                            //Log this?
                            LoggingAPI.Exception(exception, new { });
                        }
                        _bbsDataCore.RecordDisconnection(CallLogId);

                        //Close Out
                        WriteLine("~l1~c1Logging out~c2...");
                        LoggingAPI.Information(_remoteAddress + ": " + currentUser.Username + "(" + currentUser.Id.ToString() + ")" + " logged out.");
                        currentArea = "Logging out";
                        //Send end screen
                        SendFileForTermType("Goodbye", true);
                        sysopIdentified = false;
                        //Thread.Sleep(1000 * 3);
                        currentArea = "Disconnected.";
                        //SlackLogMessage(CurrentUser.Username + " logged off.");
                    }
                    HangUp();
                }
                catch (Exception exception)
                {
                    LoggingAPI.Exception(exception, new { });
                }
                return true;
            }
            return false;
        }

        //public bool IsSlackEnabled() { return _slackEnabled; }

        //public void SlackLogMessage(string message)
        //{
        //    if (_slackEnabled) _slackIntegration.LogMessage(message);
        //}

    }
}
