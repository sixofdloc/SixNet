using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public AccessGroup GetAccessGroupById(int id)
        {
            try
            {
                return _bbsDataContext.AccessGroups.FirstOrDefault(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + id + ")", e);
                return null;
            }
        }


        //public AccessGroup GetAccessGroup(int level)
        //{
        //    try
        //    {
        //        return _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(level));
        //    }
        //    catch (Exception e)
        //    {
        //        LoggingAPI.Error("(" + level + "): " + e);
        //        return null;
        //    }
        //}

        public List<AccessGroup> ListAccessGroups()
        {
            try
            {
                return _bbsDataContext.AccessGroups.ToList();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception,new { });
                return null;
            }
        }

        public bool CreateAccessGroup(AccessGroup accessGroup)
        {
            bool accessGroupCreated = false;
            try
            {

                _bbsDataContext.AccessGroups.Add(accessGroup);
                _bbsDataContext.SaveChanges();
                accessGroupCreated = true;
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { accessGroup }); 
            }
            return accessGroupCreated;
        }

        public bool UpdateAccessGroup(AccessGroup accessGroup)
        {
            bool accessGroupUpdated = false;
            try
            {

                var oldAccessGroup = GetAccessGroupById(accessGroup.Id);
                if (oldAccessGroup != null)
                {
                    //oldAccessGroup.AccessGroupNumber = accessGroup.AccessGroupNumber;
                    oldAccessGroup.CallsPerDay = accessGroup.CallsPerDay;
                    oldAccessGroup.Description = accessGroup.Description;
                    oldAccessGroup.AllowRemoteMaintenance = accessGroup.AllowRemoteMaintenance;
                    oldAccessGroup.AllowSysOp = accessGroup.AllowSysOp;
                    oldAccessGroup.MinutesPerCall = accessGroup.MinutesPerCall;
                    oldAccessGroup.Title = accessGroup.Title;
                    oldAccessGroup.Description = accessGroup.Description;
                    _bbsDataContext.SaveChanges();
                    accessGroupUpdated = true;
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { accessGroup }); 
            }
            return accessGroupUpdated;
        }

    }
}
