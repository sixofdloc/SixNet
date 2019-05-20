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

        public PFileArea GetPFileArea(int? id)
        {
            PFileArea result = null;
            try
            {
                result = _bbsDataContext.PFileAreas.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
                result = null;
            }
            return result;
        }

        public List<PFileDetail> ListPFilesForAreas(int? area)
        {
            List<PFileDetail> pfilelist = new List<PFileDetail>();
            try
            {

                pfilelist = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId.Equals(area)).ToList();
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
                pfilelist = new List<PFileDetail>();
            }

            return pfilelist;

        }

        public PFileDetail GetPFileDetailByAreaAndNumber(int? area, int number)
        {
            PFileDetail pf = null;
            try
            {
                pf = _bbsDataContext.PFileDetails.FirstOrDefault(p => p.PFileAreaId.Equals(area) //& p.PFileNumber.Equals(number)
                );
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
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
        public List<AreaListRow> PFileListArea(int? area, int userid)
        {
            //List all files and areas in the current area
            var response = new List<AreaListRow>();
            try
            {

                //User u = GetUserById(userid);
                //User must have at least one access group that matches the gfile area
                List<PFileArea> pfileAreas = _bbsDataContext.PFileAreas.Where(p => p.ParentAreaId == area).ToList();
                List<PFileDetail> pfileDetails = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId == area).ToList();
                int listId = 1;
                foreach (PFileArea pfileArea in pfileAreas)
                {
                    listId++;
                    response.Add(new AreaListRow(pfileArea, listId, AreaListRowType.Area));
                }
                foreach (PFileDetail pfileDetail in pfileDetails)
                {

                    listId++;
                    response.Add(new AreaListRow(pfileDetail, listId, AreaListRowType.Entry));
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
            }
            return response;
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
            bool b = false;
            try
            {
                if (!PFileExistsInArea(areaId, fileName))
                {
                    if (File.Exists(filePath + fileName))
                    {
                        PFileDetail pfd = new PFileDetail()
                        {
                            PFileAreaId = areaId,
                            Filename = fileName,
                            FilePath = filePath,
                            Title = title,
                            Description = description
                        };
                        _bbsDataContext.PFileDetails.Add(pfd);
                        _bbsDataContext.SaveChanges();
                        b = true;
                    }
                }
            }
            catch (Exception e)
            {
                b = false;
                LoggingAPI.LogEntry("Exception in DataInterface.AddGFile: " + e);
            }
            return b;
        }

    }
}
