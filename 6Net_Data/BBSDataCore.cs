using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using MySql.Data.MySqlClient;
using Net_Data.Models;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        private readonly BBSDataContext _bbsDataContext;
        private readonly MySqlConnection _mysqlConnection;

        public BBSDataCore(string ConnectionString)
        {
            _mysqlConnection = new MySqlConnection(ConnectionString);
            _bbsDataContext = new BBSDataContext(_mysqlConnection, false);
        }

        //public BBSDataContext DataContext()
        //{
        //    return _bbsDataContext;
        //}

        public bool ValidateConnection() {
            var result = false;
            if (_bbsDataContext.Database.Connection.State == ConnectionState.Open)
            {
                result = true;
            }
            else
            {
                try
                {
                    _bbsDataContext.Database.Connection.Open();
                    result = _bbsDataContext.Database.Connection.State == ConnectionState.Open;
                    _bbsDataContext.Database.Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    result = false;
                }
            }
            return result;
        }


        #region Pass-thrus
        public List<MessageBaseArea> MessageBaseAreas()
        {
            return _bbsDataContext.MessageBaseAreas.OrderBy(p => p.Id).ToList();
        }

        public List<MessageBase> MessageBases()
        {
            return _bbsDataContext.MessageBases.OrderBy(p => p.Id).ToList();
        }

        public List<CallLog> CallLogs()
        {
            return _bbsDataContext.CallLogs.OrderByDescending(p => p.Id).ToList();
        }

        public List<AccessGroup> AccessGroups()
        {
            return _bbsDataContext.AccessGroups.OrderBy(p => p.Title).ToList();
        }

        public List<User> Users()
        {
            return _bbsDataContext.Users.OrderBy(p => p.Id).ToList();
        }


        #endregion

    }
}
