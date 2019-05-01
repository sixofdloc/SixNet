using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public List<NewsItem> GetNews(DateTime fromdate)
        {
            var newsItems = new List<NewsItem>();
            try
            {
                newsItems = _bbsDataContext.NewsItems.Where(p => p.Sent > fromdate).OrderBy(p => p.Sent).ToList();
            } catch (Exception ex)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetNews: " + ex.ToString());
            }
            return newsItems;
        }
    }
}
