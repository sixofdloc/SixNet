using System;
using System.Threading;
using Net_Data;
using Net_Logger;

namespace Net
{
    class MainClass
    {
        private static BBSDataCore _core;
        private static string _connectionString = "";
        private static bool quitFlag = false;
        private static BBSDatabaseConfiguration _bbsDatabaseConfiguration;

        public static void Main(string[] args)
        {

            Console.Clear();
            LoggingAPI.Information("SixNet BBS, Starting up...");
            _bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig("./");
            if (_bbsDatabaseConfiguration == null)
            {
                LoggingAPI.Fatal("Run setup utility first.");
                return;
            }
            _connectionString = BBSDatabaseConfiguration.BuildConnectionString(_bbsDatabaseConfiguration);
            LoggingAPI.Init("./Logs/");
            if (BBSDatabaseConfiguration.IsDatabaseSetup(_connectionString))
            {
                LoggingAPI.Information("Database configured.");
                _core = new BBSDataCore(_connectionString);
            }
            else
            {
                LoggingAPI.Fatal("Database not configured - run setup utility.");
                return;
            }
            var config = _core.GetBBSConfig();
            LoggingAPI.Information("Software started.");
            try
            {
                quitFlag = false;
                BBSServer bbsServer = new BBSServer(_connectionString,config.BBSPort,"BBS Server");
                bbsServer.Start();
                while (!quitFlag)
                {
                    Thread.Sleep(100);
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey().Key == ConsoleKey.Q)
                        {
                            quitFlag = true;
                            bbsServer.Stop();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
               LoggingAPI.FatalException(e,"Main BBS thread stopped");
            }
            finally
            {
                LoggingAPI.Information("Software shutdown.");
            }
        }
    }
}
