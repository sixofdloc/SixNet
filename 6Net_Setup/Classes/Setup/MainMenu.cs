using System;
using Net_Data;

namespace Net_Setup.Classes.Setup
{
    public class MainMenu
    {
        private BBSDataCore _core;
        private BBSDatabaseConfiguration _configuration;
        private readonly string _basePath;

        private bool _exitFlag;

        public MainMenu(BBSDatabaseConfiguration configuration, string basePath)
        {
            _basePath = basePath;
            _configuration = configuration;
        }

        public void Menu()
        {
            _exitFlag = false;
            while (!_exitFlag)
            {
                Console.Clear();
                Console.WriteLine("SixNet Setup Utility");
                Utils.Divider();
                if (_configuration.IsDatabaseSetup())
                {
                    Console.WriteLine("Database configured.");
                    _core = new BBSDataCore(_configuration.ConnectionString());
                }
                else
                {
                    Console.WriteLine("Database not configured.");
                }
                Utils.Divider();
                Console.WriteLine("1. Configure  Database");
                if (_configuration.IsDatabaseSetup())
                {
                    Console.WriteLine("2. Configure BBS");
                    Console.WriteLine("3. Setup Message Bases");
                    Console.WriteLine("4. Setup UD Bases");
                    Console.WriteLine("5. Setup GFile Areas");
                    Console.WriteLine("6. Setup PFile Areas");
                    Console.WriteLine("S. Save Configuration");
                }
                Console.WriteLine("X. Exit");
                var selection = Utils.Input("Select", "");
                switch (selection.ToUpper())
                {
                    case "1":
                        new DatabaseSetup(_configuration).SetupDatabase();
                        break;
                    case "2":
                        new BBSSetup(_core).SetupBBS();
                        break;
                    case "3":
                        new MessageBaseSetup(_core).SetupMessageBases();
                        break;
                    case "5":
                        new GFilesSetup(_core).SetupGFileAreas();
                        break;
                    case "6":
                        new PFilesSetup(_core).SetupPFileAreas();
                        break;
                    case "S":
                        BBSDatabaseConfiguration.SaveConfig(_basePath, _configuration);
                        break;
                    case "X":
                        _exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
