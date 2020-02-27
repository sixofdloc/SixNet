using System;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public void NewFeedback(string subject, string body, int fromUser)
        {
            try
            {
                Feedback f = new Feedback() { Subject = subject, Body = body, UserId = fromUser, Posted = DateTime.Now, Read = false };
                _bbsDataContext.Feedbacks.Add(f);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Exception", e);
            }
        }
    }
}
