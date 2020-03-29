using System;
using System.Linq;
using Net_Data.Models;
using Net_Logger;
using Net_StringUtils;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public User Login(string userName, string passWord)
        {
            User user = null;
            try
            {
                var username = Utils.ToSQL(userName);
                var password = Utils.ToSQL(passWord);

                user = _bbsDataContext.Users.FirstOrDefault(p => p.Username.ToUpper().Equals(username.ToUpper()) && p.HashedPassword.Equals(password));
                user.Username = Utils.FromSQL(user.Username);
                user.HashedPassword = Utils.FromSQL(user.HashedPassword);
                user.RealName = Utils.FromSQL(user.RealName);
                user.Email = Utils.FromSQL(user.Email);
                user.ComputerType = Utils.FromSQL(user.ComputerType);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception,new { userName });
                user = null;
            }
            return user;
        }

        public bool ValidNewUsername(string userName)
        {
            var result = false;
            try
            {
                var sqlUserName = Utils.ToSQL(userName);
                result = (_bbsDataContext.Users.Count(p => p.Username.ToUpper().Equals(sqlUserName.ToUpper())) == 0);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception,new { userName });
                result = false;
            }

            return result;
        }

        public User SaveNewUser(string userName, string password, string realName, string email, string computerType, string ip, string webPage)
        {
            User newUser = null;
            try
            {
                var user = new User()
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
                _bbsDataContext.Users.Add(user);
                _bbsDataContext.SaveChanges();
                newUser = GetUserByName(userName);

            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception,new { userName, password, email, computerType, ip, webPage });
                newUser = null;
            }
            return newUser;
        }

        public bool CreateUser(User user)
        {
            bool result = false;
            try
            {
                _bbsDataContext.Users.Add(user);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { user }); 
            }
            return result;
        }

        public User SaveUser(User user)
        {
            User result = null;
            try
            {
                if (user.Id == 0)
                {
                    _bbsDataContext.Users.Add(user);
                }
                _bbsDataContext.SaveChanges();
                result = user;
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { user }); 
                result = null;
            }
            return result;
        }

        public User GetUserById(int userId)
        {
            try
            {
                return _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userId));
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { userId }); 
                return null;
            }
        }

        public User GetUserByName(string userName)
        {
            try
            {
                return _bbsDataContext.Users.FirstOrDefault(p => p.Username.ToUpper().Equals(userName.ToUpper()));
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { userName }); 
                return null;
            }
        }
    }
}
