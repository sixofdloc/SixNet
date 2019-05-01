using System;
using System.Linq;
using Net_Data.Models;
using Net_Logger;
using Net_StringUtils;

namespace Net_Data
{
    public partial class BBSDataCore
    {

        #region Users

        public User Login(string un, string pw)
        {
            User u = null;
            try
            {
                var username = Utils.ToSQL(un);
                var password = Utils.ToSQL(pw);

                u = _bbsDataContext.Users.FirstOrDefault(p => p.Username.ToUpper().Equals(username.ToUpper()) && p.HashedPassword.Equals(password));
                u.Username = Utils.FromSQL(u.Username);
                u.HashedPassword = Utils.FromSQL(u.HashedPassword);
                u.RealName = Utils.FromSQL(u.RealName);
                u.Email = Utils.FromSQL(u.Email);
                u.ComputerType = Utils.FromSQL(u.ComputerType);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.Login: " + e.ToString());
                u = null;
            }
            return u;
        }

        public bool ValidNewUsername(string s)
        {
            var b = false;
            try
            {
                var uname = Utils.ToSQL(s);
                b = (_bbsDataContext.Users.Count(p => p.Username.ToUpper().Equals(uname.ToUpper())) == 0);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ValidNewUsername: " + e.ToString());
                b = false;
            }

            return b;
        }

        public User SaveNewUser(string userName, string password, string realName, string email, string computerType, string ip, string webPage)
        {
            User newUser = null;
            try
            {
                var u = new User()
                {
                    Username = userName,
                    HashedPassword = password,
                    RealName = realName,
                    Email = email,
                    ComputerType = computerType,
                    WebPage = webPage,
                    LastConnection = DateTime.Now,
                    LastDisconnection = DateTime.Now,
                    LastConnectionIP = ip
                };
                _bbsDataContext.Users.Add(u);
                _bbsDataContext.SaveChanges();
                newUser = GetUserByName(userName);

            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
                newUser = null;
            }
            return newUser;
        }

        public bool CreateUser(User user)
        {
            bool b = false;
            try
            {
                _bbsDataContext.Users.Add(user);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", user, e);
            }
            return b;
        }

        public User SaveUser(User user)
        {
            User result = null;
            try
            {
                if (user.Id == 0)
                {

                    //var newUser = false;
                    //var dbUser = GetUserById(user.Id);
                    //if (dbUser == null)
                    //{
                    //    dbUser = new User();
                    //}

                    //dbUser.Username = user.Username;
                    //dbUser.HashedPassword = user.HashedPassword;
                    //dbUser.LastConnection = user.LastConnection;
                    //dbUser.LastConnectionIP = user.LastConnectionIP;
                    //dbUser.LastDisconnection = user.LastDisconnection;
                    //dbUser.RealName = user.RealName;
                    //dbUser.ComputerType = user.ComputerType;
                    //dbUser.Email = user.Email;
                    //dbUser.WebPage = user.WebPage;

                    //if (newUser)
                    //{
                    _bbsDataContext.Users.Add(user);
                }
                _bbsDataContext.SaveChanges();
                result = user;
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", user, e);
                result = null;
            }
            return result;
        }

        public User GetUserById(int id)
        {
            try
            {
                return _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + id + ")", e);
                return null;
            }
        }

        public User GetUserByName(string username)
        {
            try
            {
                return _bbsDataContext.Users.FirstOrDefault(p => p.Username.ToUpper().Equals(username.ToUpper()));
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + username + ")", e);
                return null;
            }
        }

        #endregion

    }
}
