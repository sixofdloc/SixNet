using System;
using System.Collections.Generic;
using System.Timers;
using System.IO;

namespace SixNet_Logger
{
    public static class LoggingAPI
    {
        private static List<string> logqueue = null;
        private static Timer logtick = null;
        private static object loglock = null;
        public static string ErrMsg = "";
        public static string LogPath = "Logs//";
        public static bool Init(string logpath)
        {
            bool b = false;
            try
            {
                LogPath = logpath;
                logqueue = new List<string>();
                logtick = new Timer(1000);
                logtick.Elapsed += new ElapsedEventHandler(logtick_Elapsed);
                loglock = new Object();
                logtick.Enabled = true;
                b = true;
            }
            catch (Exception e)
            {
                ErrMsg = e.Message;
                b = false;
            }
            return b;
        }

        public static void FlushQueue()
        {
            lock (loglock)
            {
                TextWriter tw = new StreamWriter(LogPath + DateTime.Today.ToString("yyyy-MM-dd") + ".log", true);
                foreach (string s in logqueue)
                {
                    tw.WriteLine(s);
                }
                tw.Close();
                logqueue.Clear();
            }

        }

        static void logtick_Elapsed(object sender, ElapsedEventArgs e)
        {
            logtick.Enabled = false;
            try
            {
                FlushQueue();
            }
#pragma warning disable 168
            catch (Exception ex)
            {
                //Really nothing we can do here?  Notify someone?
                //Logger.LogEntry("Game1.DebounceKey Exception - " + e.Message + " | " + e.StackTrace);
            }
#pragma warning restore 168
            logtick.Enabled = true;
        }

        public static void LogEntry(string s)
        {
            DateTime Start = DateTime.Now;
            if (logtick == null) Init("Logs//");
            lock (loglock)
            {
                logqueue.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + s);
            }
        }
    }
}
