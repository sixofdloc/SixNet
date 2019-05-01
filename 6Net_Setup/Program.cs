using System;
using System.Data;
using System.Linq;
using Net_Data;
using Net_Data.Models;
using Net_Logger;

namespace Net_Setup
{
    partial class MainClass
    {
        private static BBSDataCore _core;
        private static bool _exitFlag;

        private static string _basePath = "./";
        private static string _connectionString = "";
        private static BBSDatabaseConfiguration _bbsDatabaseConfiguration;

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("SixNet Setup Utility");
            _bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig(_basePath);
            if (_bbsDatabaseConfiguration == null)
            {
                _basePath = Input("BBS Base Path",_basePath);
                _bbsDatabaseConfiguration = BBSDatabaseConfiguration.LoadConfig(_basePath);
                if (_bbsDatabaseConfiguration == null) _bbsDatabaseConfiguration = new BBSDatabaseConfiguration();
            }
            _connectionString = BBSDatabaseConfiguration.BuildConnectionString(_bbsDatabaseConfiguration);

            LoggingAPI.Init(_basePath);

            MainMenu();
        }

        private static string Input(string prompt, string defaultValue)
        {
            if (prompt != "")
            {
                var promptStr = prompt;
                if (defaultValue != "")
                {
                    promptStr = promptStr + " [" + defaultValue + "]";
                }
                Console.Write(promptStr + ": ");
            }
            var response = Console.ReadLine();
            if (response == "") response = defaultValue;
            return response;
        }

        private static void EnterToContinue()
        {
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
        }

        private static void MainMenu()
        {
            _exitFlag = false;
            while (!_exitFlag)
            {
                Console.Clear();
                Console.WriteLine("SixNet Setup Utility");
                Divider();
                if (BBSDatabaseConfiguration.IsDatabaseSetup(_connectionString))
                {
                    Console.WriteLine("Database configured.");
                    _core = new BBSDataCore(_connectionString);
                }
                else
                {
                    Console.WriteLine("Database not configured.");
                }
                Divider();
                Console.WriteLine("1. Configure  Database");
                if (BBSDatabaseConfiguration.IsDatabaseSetup(_connectionString))
                {
                    Console.WriteLine("2. Configure BBS");
                    Console.WriteLine("3. Setup Message Bases");
                    Console.WriteLine("4. Setup UD Bases");
                    Console.WriteLine("5. Setup GFile Areas");
                    Console.WriteLine("6. Setup PFile Areas");
                    Console.WriteLine("S. Save Configuration");
                }
                Console.WriteLine("X. Exit");
                var selection = Input("Select","");
                switch (selection.ToUpper())
                {
                    case "1":
                        SetupDatabase();
                        break;
                    case "2":
                        SetupBBS();
                        break;
                    case "5":
                        SetupGFileAreas();
                        break;
                    case "S":
                        BBSDatabaseConfiguration.SaveConfig(_basePath, _bbsDatabaseConfiguration);
                        break;
                    case "X":
                        _exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private static void SetupBBS()
        {

            var bbsConfig = _core.GetBBSConfig();
            User sysOpUser = null;
            if (bbsConfig == null)
            {
                bbsConfig = new BBSConfig();
                sysOpUser = new User();
            }
            else
            {
                sysOpUser = _core.GetUserById(bbsConfig.SysOpUserId);
                if (sysOpUser == null)
                {
                    sysOpUser = new User();
                }
            }

            sysOpUser.Username = Input("Enter the SysOp's username", sysOpUser.Username);
            sysOpUser.HashedPassword = Input("Enter the SysOp's password", sysOpUser.HashedPassword);
            sysOpUser.LastConnection = DateTime.Now;
            sysOpUser.LastConnectionIP = "localhost";
            sysOpUser.LastDisconnection = DateTime.Now.AddSeconds(1);
            sysOpUser.RealName = Input("Enter the SysOp's real name", sysOpUser.RealName);
            sysOpUser.ComputerType = Input("Enter the SysOp's main computer type", sysOpUser.ComputerType);
            sysOpUser.Email = Input("Enter the SysOp's private email address", sysOpUser.Email);
            sysOpUser.WebPage = Input("Enter the SYsOp's webpage address", sysOpUser.WebPage);

            sysOpUser = _core.SaveUser(sysOpUser);
            if (sysOpUser != null)
            {
                bbsConfig.SysOpUserId = sysOpUser.Id;


                bbsConfig.BBSName = Input("Enter the BBS name", bbsConfig.BBSName);
                bbsConfig.BBSUrl = Input("Enter the BBS URL (no port)", bbsConfig.BBSUrl);

                var Port = "";
                bool portComplete = false;
                while (!portComplete)
                {
                    Port = Input("Enter the BBS port", bbsConfig.BBSPort.ToString());
                    if (int.TryParse(Port, out int intPort))
                    {
                        bbsConfig.BBSPort = intPort;
                        portComplete = true;
                    }
                }
                bbsConfig.SysOpPublicHandle = Input("Enter the SysOp's public handle", bbsConfig.SysOpPublicHandle);
                bbsConfig.SysOpEmail = Input("Enter the SysOp's public email address", bbsConfig.SysOpEmail);
                bbsConfig.SysOpMenuPassword = Input("Enter the password for the SysOp menu", bbsConfig.SysOpMenuPassword);
                _core.SaveBBSConfig(bbsConfig);
            }
        }
        private static void SetupDatabase()
        {
            var dbServer = Input("Enter your database server name",_bbsDatabaseConfiguration.DatabaseName);
            var dbDatabaseName = Input("Enter the database name",_bbsDatabaseConfiguration.DatabaseServer);
            var dbDatabaseUser = Input("Enter the database user",_bbsDatabaseConfiguration.DatabaseUsername);
            var dbDatabasePass = Input("Enter the database password",_bbsDatabaseConfiguration.DatabasePassword);

            _bbsDatabaseConfiguration.DatabaseName = dbDatabaseName;
            _bbsDatabaseConfiguration.DatabaseServer = dbServer;
            _bbsDatabaseConfiguration.DatabaseUsername = dbDatabaseUser;
            _bbsDatabaseConfiguration.DatabasePassword = dbDatabasePass;
            _connectionString = BBSDatabaseConfiguration.BuildConnectionString(_bbsDatabaseConfiguration);
        }

        private static void Divider()
        {
            Console.WriteLine("===============================================================================");
        }


    }
}
