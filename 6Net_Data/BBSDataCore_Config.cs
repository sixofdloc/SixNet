using System;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public BBSConfig GetBBSConfig()
        {
            BBSConfig result = null;
            try
            {
                result =  _bbsDataContext.BBSConfigs.FirstOrDefault(p => true);
            }
            catch (Exception ex)
            {
                LoggingAPI.Error("Exception: ", ex);
                result = null;
            }
            return result;
        }

        public void SaveBBSConfig(BBSConfig bbsConfig)
        {
            if (bbsConfig.Id == 0)
            {
                _bbsDataContext.BBSConfigs.Add(bbsConfig);
            } 
            _bbsDataContext.SaveChanges();
        }

    }
}
