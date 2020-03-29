using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Net_Data.Classes;
using Net_Data.Enums;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public List<PFileArea> PFileAreas() => _bbsDataContext.PFileAreas.ToList();
        public List<PFileDetail> PFileDetails() => _bbsDataContext.PFileDetails.ToList();
        public List<PFileDetail> PFileDetails(int? areaId) => _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId == areaId).ToList();

        public PFileArea GetPFileArea(int? areaId)
        {
            PFileArea pfileArea = null;
            try
            {
                pfileArea = _bbsDataContext.PFileAreas.FirstOrDefault(p => p.Id == areaId);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId });
                pfileArea = null;
            }
            return pfileArea;
        }

        public List<PFileDetail> ListPFilesForAreas(int? areaId)
        {
            List<PFileDetail> pfileDetailList = new List<PFileDetail>();
            try
            {

                pfileDetailList = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId.Equals(areaId)).ToList();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId });
                pfileDetailList = new List<PFileDetail>();
            }

            return pfileDetailList;

        }

        public PFileDetail GetPFileDetailByAreaAndNumber(int? areaId, int number)
        {
            PFileDetail pf = null;
            try
            {
                pf = _bbsDataContext.PFileDetails.FirstOrDefault(p => p.PFileAreaId.Equals(areaId) //& p.PFileNumber.Equals(number)
                );
            }
            catch (Exception e)
            {
                LoggingAPI.Exception(e, new { areaId, number }); 
                pf = null;
            }
            return pf;
        }

        //public int PFile_ParentArea(int? area)
        //{
        //    int i = -1;
        //    try
        //    {

        //        PFileArea gfa = _bbsDataContext.PFileAreas.FirstOrDefault(p => p.ParentAreaId.Equals(area));
        //        if (gfa != null)
        //        {
        //            i = gfa.ParentAreaId;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        LoggingAPI.LogEntry("Exception in DataInterface.PFile_ParentArea: " + e.Message);
        //    }
        //    return i;
        //}
        public List<AreaListRow> PFileListArea(int? areaId, int userId)
        {
            //List all files and areas in the current area
            var listForThisArea = new List<AreaListRow>();
            try
            {

                //User u = GetUserById(userid);
                //User must have at least one access group that matches the gfile area
                List<PFileArea> pfileAreas = _bbsDataContext.PFileAreas.Where(p => p.ParentAreaId == areaId).ToList();
                List<PFileDetail> pfileDetails = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId == areaId).ToList();
                int listId = 1;
                foreach (PFileArea pfileArea in pfileAreas)
                {
                    listId++;
                    listForThisArea.Add(new AreaListRow(pfileArea, listId, AreaListRowType.Area));
                }
                foreach (PFileDetail pfileDetail in pfileDetails)
                {

                    listId++;
                    listForThisArea.Add(new AreaListRow(pfileDetail, listId, AreaListRowType.Entry));
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId, userId, listForThisArea });
            }
            return listForThisArea;
        }

        public bool PFileExistsInArea(int? areaid, string filename)
        {

            return _bbsDataContext.PFileDetails.Any(p=>p.Filename == filename); //p => p.PFileAreaId ==areaid && p.Filename.Equals(filename));

        }

        public PFileDetail GetPFileDetail(int id)
        {
            return _bbsDataContext.PFileDetails.FirstOrDefault(p => p.Id.Equals(id));
        }

        public PFileArea CreatePFileArea(string title, string description, int? parentAreaId)
        {
            var pfileArea = new PFileArea()
            {
                Title = title,
                Description = description,
                ParentAreaId = parentAreaId
            };
            _bbsDataContext.PFileAreas.Add(pfileArea);
            _bbsDataContext.SaveChanges();
            return pfileArea;
        }

        public bool AddPFileDetail(int areaId, string filePath, string fileName, string title, string description)
        {
            //filename must include relative path from bbs
            //determines file size on its own
            //Will not allow re-add of the same filename to the same area
            bool pfileDetailAdded = false;
            try
            {
                if (!PFileExistsInArea(areaId, fileName))
                {
                    if (File.Exists(filePath + fileName))
                    {
                        PFileDetail pfileDetail = new PFileDetail()
                        {
                            PFileAreaId = areaId,
                            Filename = fileName,
                            FilePath = filePath,
                            Title = title,
                            Description = description
                        };
                        _bbsDataContext.PFileDetails.Add(pfileDetail);
                        _bbsDataContext.SaveChanges();
                        pfileDetailAdded = true;
                    }
                }
            }
            catch (Exception exception)
            {
                pfileDetailAdded = false;
                LoggingAPI.Exception(exception, new { areaId, filePath, fileName, title, description }); 
            }
            return pfileDetailAdded;
        }

    }
}
