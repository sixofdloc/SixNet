using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Net_Logger
{
    public static class LoggingAPI
    {

        public static bool Init(string logPath)
        {
            bool b = true;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File(logPath + "bbslog-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            return b;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Exception(Exception ex, object methodParams)
        {
            var jsonStr = JsonConvert.SerializeObject(methodParams);
            Log.Error(ex, "Exception in " + GetSendingMethod() + ", params: {jsonStr} " ,jsonStr);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void FatalException(Exception ex, object methodParams)
        {
            var jsonStr = JsonConvert.SerializeObject(methodParams);
            Log.Fatal(ex, "Exception in " + GetSendingMethod() + ", params: {jsonStr} ", jsonStr);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Verbose(string entryText, params Object[] attachments)
        {
            Log.Verbose(GetSendingMethod() + ": " + entryText,attachments);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Debug(string entryText, params Object[] attachments)
        {
            Log.Debug(GetSendingMethod() + ": " + entryText, attachments);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Information(string entryText, params Object[] attachments)
        {
            Log.Information(GetSendingMethod() + ": " + entryText, attachments);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Warning(string entryText, params Object[] attachments)
        {
            Log.Warning(GetSendingMethod() + ": " + entryText, attachments);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Error(string entryText, params Object[] attachments)
        {
            Log.Error(GetSendingMethod() + ": " + entryText, attachments);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Fatal(string entryText, params Object[] attachments)
        {
            Log.Fatal(GetSendingMethod() + ": " + entryText, attachments);
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetSendingMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            //0 would be this, 1 would be the logging proc, 2 would be what called the logging proc.

            return stackFrame.GetMethod().ReflectedType.FullName + "." + stackFrame.GetMethod().Name;
        }
    }
}
