using System;
using Net_Data;
using Net_Logger;

namespace Net_Setup.Classes.Setup
{
    public class DatabaseSetup
    {
        private BBSDatabaseConfiguration _configuration;
        public DatabaseSetup(BBSDatabaseConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SetupDatabase()
        {
            var result = "";
            try
            {
                var dbServer = Utils.Input("Enter your database server name", _configuration.DatabaseName);
                var dbDatabaseName = Utils.Input("Enter the database name", _configuration.DatabaseServer);
                var dbDatabaseUser = Utils.Input("Enter the database user", _configuration.DatabaseUsername);
                var dbDatabasePass = Utils.Input("Enter the database password", _configuration.DatabasePassword);

                _configuration.DatabaseName = dbDatabaseName;
                _configuration.DatabaseServer = dbServer;
                _configuration.DatabaseUsername = dbDatabaseUser;
                _configuration.DatabasePassword = dbDatabasePass;
                result = BBSDatabaseConfiguration.BuildConnectionString(_configuration);
            }
            catch (Exception ex)
            {
                LoggingAPI.Exception(ex, new { });
                result = "";
            }
            return result;
        }

    }
}
