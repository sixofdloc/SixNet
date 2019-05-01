using System;
using System.IO;
using Newtonsoft.Json;

namespace Net_Data
{
    public class BBSDatabaseConfiguration
    {
        //Database Details
        public string DatabaseServer { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }

        [JsonIgnore]
        private const string configFilename = "bbsconfig.txt";

        public static BBSDatabaseConfiguration LoadConfig(string filePath)
        {
            BBSDatabaseConfiguration result = null;
            try
            {
                var fileText = File.ReadAllText(filePath + configFilename);
                result = JsonConvert.DeserializeObject<BBSDatabaseConfiguration>(fileText);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public static bool SaveConfig(string filePath, BBSDatabaseConfiguration config)
        {
            var result = false;
            try
            {
                var fileText = JsonConvert.SerializeObject(config);
                File.WriteAllText(filePath + configFilename, fileText);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public  static string BuildConnectionString(BBSDatabaseConfiguration _bbsConfig)
        {
            return $"server={_bbsConfig.DatabaseServer};port=3306;database={_bbsConfig.DatabaseName};uid={_bbsConfig.DatabaseUsername};password={_bbsConfig.DatabasePassword}";
        }

        public static bool IsDatabaseSetup(string connectionString)
        {
            var result = false;
            try
            {
                BBSDataCore _core = new BBSDataCore(connectionString);
                result = (_core.ValidateConnection());
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }


}
