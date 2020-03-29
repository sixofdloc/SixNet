using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public void AddGraffiti(string graffitiText, int userId)
        {
            try
            {
                Graffiti graffiti = new Graffiti() { Content = graffitiText, Posted = DateTime.Now, UserId = userId };
                _bbsDataContext.Graffiti.Add(graffiti);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception,new { graffitiText, userId });
            }
        }



        public List<Tuple<string,string>> GetGraffiti()
        {
            var graffitiList = new List<Tuple<string,string>>();
            try
            {
                var graffitis = _bbsDataContext.Graffiti.OrderByDescending(p => p.Posted).Take(10).ToList();
                foreach (var graffiti in graffitis) {
                    if (graffiti.User == null) {
                        var user = GetUserById(graffiti.UserId);
                        if (user != null) {
                            graffitiList.Add(new Tuple<string, string>(user.Username, graffiti.Content));
                        }
                    }
                    else {
                        graffitiList.Add(new Tuple<string, string>(graffiti.User.Username, graffiti.Content));
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { graffitiList });
            }
            return graffitiList;
        }

    }
}
