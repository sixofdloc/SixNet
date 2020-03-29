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
        public List<GFileArea> GfileAreas() => _bbsDataContext.GFileAreas.ToList();

        public GFileArea GetGFileArea(int gfileAreaId)
        {
            GFileArea gfileArea = null;
            try
            {
                gfileArea = _bbsDataContext.GFileAreas.FirstOrDefault(p => p.Id == gfileAreaId);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { gfileAreaId, gfileArea }); 
                gfileArea = null;
            }
            return gfileArea;
        }

        public int GFile_ParentArea(int gfileAreaId)
        {
            int i = -1;
            try
            {
                GFileArea gfileArea = _bbsDataContext.GFileAreas.FirstOrDefault(p => p.Id.Equals(gfileAreaId));
                if (gfileArea != null)
                {
                    i = gfileArea.ParentAreaId==null?-1:(int)gfileArea.ParentAreaId;
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { gfileAreaId }); 
            }
            return i;
        }

        public List<AreaListRow> GFileListArea(int? gfileAreaId, int userId)
        {
            //List all files and areas in the current area
            var listForThisArea = new List<AreaListRow>();
            try
            {

                //User u = GetUserById(userid);
                //User must have at least one access group that matches the gfile area
                List<GFileArea> gfileAreas = _bbsDataContext.GFileAreas.Where(p => p.ParentAreaId==gfileAreaId ).ToList();
                List<GFileDetail> gfileDetails = _bbsDataContext.GFileDetails.Where(p => p.GFileAreaId==gfileAreaId).ToList();
                int listId = 1;
                foreach (GFileArea gfileArea in gfileAreas)
                {
                    listId++;
                    listForThisArea.Add(new AreaListRow(gfileArea,listId,AreaListRowType.Area));
                }
                foreach (GFileDetail gfileDetail in gfileDetails)
                {

                    listId++;
                    listForThisArea.Add(new AreaListRow(gfileDetail,listId,AreaListRowType.Entry));
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { gfileAreaId, userId, listForThisArea });
            }
            return listForThisArea;
        }

        public bool AddGFileDetail(int gfileAreaId, string filePath, string fileName,string title, string description, string notes, bool PETSCII)
        {
            //filename must include relative path from bbs
            //determines file size on its own
            //Will not allow re-add of the same filename to the same area
            bool gfileDetailAdded = false;
            try
            {
                if (!GFileExistsInArea(gfileAreaId, fileName))
                {
                    if (File.Exists(filePath+fileName))
                    {
                        GFileDetail gfileDetail = new GFileDetail()
                        {
                            GFileAreaId = gfileAreaId,
                            Filename = fileName,
                            FilePath = filePath,
                            Added = DateTime.Now,
                            Title = title,
                            Description = description,
                            Notes = notes,
                            PETSCII = PETSCII,
                            FileSizeInBytes = (int)(new FileInfo(filePath+fileName).Length)
                        };
                        _bbsDataContext.GFileDetails.Add(gfileDetail);
                        _bbsDataContext.SaveChanges();
                        gfileDetailAdded = true;
                    }
                }
            }
            catch (Exception exception)
            {
                gfileDetailAdded = false;
                LoggingAPI.Exception(exception, new { gfileAreaId,filePath,fileName,title,description,notes,PETSCII }); 
            }
            return gfileDetailAdded;
        }
        public bool GFileExistsInArea(int areaid, string filename)
        {

            int x = _bbsDataContext.GFileDetails.Count(p => p.GFileAreaId.Equals(areaid) && p.Filename.Equals(filename));
            return (x > 0);
        }

        public GFileDetail GetGFileDetail(int id)
        {

            return _bbsDataContext.GFileDetails.FirstOrDefault(p => p.Id.Equals(id));
        }

        public GFileArea CreateGFileArea(string title, string description, int? parentAreaId)
        {
            var gfileArea = new GFileArea() { 
                    Title = title,
                    Description = description, 
                    ParentAreaId = parentAreaId 
             };
            _bbsDataContext.GFileAreas.Add(gfileArea);
            _bbsDataContext.SaveChanges();
            return gfileArea;
        }
    }
}
