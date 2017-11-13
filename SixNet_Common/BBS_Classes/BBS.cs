﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SixNet_BBS.BBS_Classes;
using SixNet_Comm;
using SixNet_BBS.Interfaces;
using SixNet_Logger;
using SixNet_BBS_Data;

namespace SixNet_BBS
{
    public class BBS : Communicator
    {
        public DateTime ConnectionTimeStamp;
        //Exposed for server to display/act on
        public User CurrentUser { get; set; }
        public bool Sysop_Identified { get; set; }
        public string CurrentArea { get; set; } //used for upper display
       
        public bool ExpertMode = false;
        
        public bool DoNotDisturb = false;
        public bool DND_Override = false;
  
        public List<string> MessageQueue = new List<string>();
        public System.Timers.Timer MessageQueueTimer = new System.Timers.Timer(1000);

        //Nuts & Bolts
        public IBBSHost Host_System;

        private readonly DataInterface _dataInterface;

        public GraffitiWall Gw { get; set; }

        public BBS(IBBSHost host_system,StateObject so, string ConnectionString)
        {
            //if (DBUpdater.InitializeDatabase())
            //{
            ConnectionTimeStamp = DateTime.Now;
            _dataInterface = new DataInterface(ConnectionString);
                Host_System = host_system;
                State_Object = so;
                MessageQueueTimer.Enabled = false;
                MessageQueueTimer.Interval = 100;
                MessageQueueTimer.Elapsed += new System.Timers.ElapsedEventHandler(MessageQueueTimer_Elapsed);
            //}
            //else
            //{
            //    State_Object = null;
            //}
        }

        void MessageQueueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!DoNotDisturb)
            {
                if (!DND_Override)
                {
                    MessageQueueTimer.Enabled = false;
                    FlushOLMQueue();
                    MessageQueueTimer.Enabled = true;
                }

            }
        }

        public void FlushOLMQueue()
        {
            bool b = MessageQueueTimer.Enabled;
            if (b) MessageQueueTimer.Enabled = false;
            List<string> all = MessageQueue.Where(p => true).ToList();
            foreach (string s in all)
            {
                WriteLine(s);
                MessageQueue.Remove(s);
            }
            MessageQueueTimer.Enabled = b;
        }

        public bool Go()
        {
            if (State_Object != null)
            {
                
                DoNotDisturb = true;
                DND_Override = false;
                MessageQueue = new List<string>();
                State_Object.AddDisconnectHandler(Disconnect);
                Connected = true;
                State_Object.AddReceiver(Receive);
                Currentchar = 0x00;
                try
                {
                    Sysop_Identified = false;
                    //Terminal Detection
                    TerminalType = new TermType_Default();
                    TermDetect td = new TermDetect(this);
                    TerminalType = td.Detect();
                    Write("~s1");
                    //Welcome Screen
                    SendFileForTermType("Welcome", true);
                    if (TerminalType.C64_Color())
                    {
                        AnyKey(true,false);
                        Write("~s2");
                    }
                    else
                    {
                        AnyKey(true,true);
                    }

                    //Login
                    Login li = new Login(this,_dataInterface);
                    CurrentUser = li.LogIn();
                    if (CurrentUser != null)
                    {
                        int CallLogId = _dataInterface.RecordConnection(CurrentUser.UserId);
                        
                        LastTen lt = new LastTen(this, _dataInterface);
                        lt.ShowLast10();
                        AnyKey(true, false);
                        
                        Gw = new GraffitiWall(this, _dataInterface);
                        Gw.DisplayWall();
                        AnyKey(true, false);

                        News ne = new News(this, _dataInterface);
                        ne.DisplayNews();
                        AnyKey(true, false);

                        Main main = new Main(this, _dataInterface);
                        try
                        {
                            MessageQueueTimer.Enabled = true;
                            Thread.Sleep(100);
                            main.MainPrompt();
                            MessageQueueTimer.Enabled = false;
                            Thread.Sleep(100);
                        }
                        catch (Exception e)
                        {
                            //Log this?
                            LoggingAPI.LogEntry("Exception in BBS.Go: " + e);
                        }
                        _dataInterface.RecordDisconnection(CallLogId);
                    }
                    //Close Out
                    WriteLine("~l1~c1Logging out~c2...");
                    CurrentArea = "Logging out";
                    //Send end screen
                    SendFileForTermType("Goodbye", true);
                    Sysop_Identified = false;
                    Thread.Sleep(1000 * 3);
                    CurrentArea = "Disconnected.";
                    HangUp();
                }
                catch (Exception e)
                {
                    LoggingAPI.LogEntry("Exception in BBS.Go: " + e.Message);
                }
                return true;
            }
            else return false;
        }



    }
}
