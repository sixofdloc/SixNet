﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;
using System.Text;

namespace SixNet_Logger
{
    class Logger
    {
        private  List<string> logqueue = null;
        private  Timer logtick = null;
        private  object loglock = null;

        private readonly string _logPath;
        private readonly string _prefix;

        public Logger(string prefix, string logpath)
        {
            _logPath = logpath;
            _prefix = prefix;
            logqueue = new List<string>();
            logtick = new Timer(1000);
            logtick.Elapsed += new ElapsedEventHandler(logtick_Elapsed);
            loglock = new Object();
            logtick.Enabled = true;
        }

        public void FlushQueue()
        {
            lock (loglock)
            {
                TextWriter tw = new StreamWriter(_logPath + DateTime.Today.ToString("yyyy-MM-dd") + _prefix + ".log", true);
                foreach (string s in logqueue)
                {
                    tw.WriteLine(s);
                }
                tw.Close();
                logqueue.Clear();
            }

        }

        void logtick_Elapsed(object sender, ElapsedEventArgs e)
        {
            logtick.Enabled = false;
            try
            {
                FlushQueue();
            }
            finally
            {
                logtick.Enabled = true;
            }
        }

        public void Entry(string s)
        {
            lock (loglock)
            {
                logqueue.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + s);
            }
        }
    }
}
