using System;
using System.Collections.Generic;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace SixNet_Logger
{
    public static class LoggingAPI
    {
        private static Logger _debugLog;
        private static Logger _errorLog;
        private static Logger _sysLog;

        public static bool Init(string logPath)
        {
            bool b = true;
            _debugLog = new Logger("DEBUG", logPath);
            _errorLog = new Logger("ERROR", logPath);
            _sysLog = new Logger("SYSLOG", logPath);
            return b;
        }

        public static void FlushQueue()
        {
            _debugLog.FlushQueue();
            _errorLog.FlushQueue();
            _sysLog.FlushQueue();
        }

        public static void LogEntry(string entryText)
        {
            _debugLog.Entry(entryText);
        }

        public static void LogEntry(string entryText, params Object[] attachments)
        {
            var attachText = "";
            foreach(Object attachment in attachments)
            {
                attachText += "\r\n\r\n" + JsonConvert.SerializeObject(attachment);
            }
            _debugLog.Entry(entryText + ", " + attachText );
        }

        public static void Error(string errorMessage)
        {
            _errorLog.Entry(GetSendingMethod()+": "+errorMessage);
        }

        public static void Error(string errorMessage, params Object[] attachments)
        {
            var attachText = "";
            foreach (Object attachment in attachments)
            {
                attachText += "\r\n\r\n" + JsonConvert.SerializeObject(attachment);
            }
            _errorLog.Entry(GetSendingMethod() + ": " + errorMessage + ", ATTACHMENTS: " + attachText);
        }

        public static void Error(Exception exception)
        {
            _errorLog.Entry(GetSendingMethod() + ": \r\n\r\n" + JsonConvert.SerializeObject(exception));
        }

        public static void SysLogEntry(string entryText)
        {
            _sysLog.Entry(entryText);
        }

        public static void SysLogEntry(string entryText, params Object[] attachments)
        {
            var attachText = "";
            foreach (Object attachment in attachments)
            {
                attachText += "\r\n\r\n" + JsonConvert.SerializeObject(attachment);
            }
            _sysLog.Entry(entryText + ", " + attachText);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetSendingMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            //0 would be this, 1 would be the logging proc, 2 would be what called the logging proc.

            return stackFrame.GetMethod().Name;
        }
    }
}
