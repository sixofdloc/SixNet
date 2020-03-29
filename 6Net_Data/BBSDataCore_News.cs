using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public List<NewsItem> GetNews(DateTime fromDate)
        {
            var newsItems = new List<NewsItem>();
            try
            {
                newsItems = _bbsDataContext.NewsItems
                                .Where(newsItem => newsItem.Posted > fromDate)
                                .OrderBy(newsItem => newsItem.Posted)
                                .ToList();
            } catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { fromDate }); 
            }
            return newsItems;
        }
    }
}
