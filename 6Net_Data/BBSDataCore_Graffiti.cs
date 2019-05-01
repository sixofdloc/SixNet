using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public void AddGraffiti(string content, int userid)
        {
            try
            {
                Graffiti graffiti = new Graffiti() { Content = content, Posted = DateTime.Now, UserId = userid };
                _bbsDataContext.Graffiti.Add(graffiti);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.AddGraffiti: " + e.ToString());
            }
        }



        public List<Tuple<string,string>> GetGraffiti()
        {
            var glist = new List<Tuple<string,string>>();
            try
            {
                var graffitis = _bbsDataContext.Graffiti.OrderByDescending(p => p.Posted).Take(10).ToList();
                foreach (var graffiti in graffitis) {
                    if (graffiti.User == null) {
                        var user = GetUserById(graffiti.UserId);
                        if (user != null) {
                            glist.Add(new Tuple<string, string>(user.Username, graffiti.Content));
                        }
                    }
                    else {
                        glist.Add(new Tuple<string, string>(graffiti.User.Username, graffiti.Content));
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetGraffiti: " + e.ToString());
            }
            return glist;
        }

    }
}
