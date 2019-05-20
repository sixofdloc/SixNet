using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public int RecordConnection(int userid)
        {
            int callLogId = -1;
            try
            {
                CallLog callLog = new CallLog() { Connected = DateTime.Now, Disconnected = DateTime.Now, UserId = userid };
                _bbsDataContext.CallLogs.Add(callLog);
                _bbsDataContext.SaveChanges();
                callLogId = callLog.Id;
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
                callLogId = -1;
            }
            return callLogId;
        }

        public void UpdateCallLog(int callLogId, int userid) //Used after new user reg
        {
            CallLog cl = _bbsDataContext.CallLogs.FirstOrDefault(p => p.Id.Equals(callLogId));
            if (cl != null)
            {
                cl.UserId = userid;
                _bbsDataContext.SaveChanges();
            }
        }

        public void RecordDisconnection(int callLogId)
        {
            CallLog cl = _bbsDataContext.CallLogs.FirstOrDefault(p => p.Id.Equals(callLogId));
            if (cl != null)
            {
                cl.Disconnected = DateTime.Now;
                _bbsDataContext.SaveChanges();
            }
        }

        public List<Tuple<string, string>> GetLastTenCalls()
        {
            var glist = new List<Tuple<string, string>>();
            try
            {
                var callList = _bbsDataContext.CallLogs.OrderByDescending(p => p.Connected) .Take(10).ToList();
                foreach (var call in callList)
                {
                    var connected = call.Connected.ToString("yyyy-MM-dd hh:mm");
                    var username = "";
                    if (call.User == null)
                    {
                        var user = GetUserById(call.UserId);
                        username = user.Username;
                    }
                    else
                    {
                        username = call.User.Username;
                    }
                    glist.Add(new Tuple<string, string>(connected, username));
                    // = callList.Select(p => new Tuple<string, string>(p.Connected.ToString("yyyy-MM-dd hh:mm"), p.User.Username)).ToList();
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
            }
            return glist;
        }
    }
}
