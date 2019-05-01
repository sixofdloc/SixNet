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

        public GFileArea GetGFileArea(int id)
        {
            GFileArea result = null;
            try
            {
                result = _bbsDataContext.GFileAreas.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
                result = null;
            }
            return result;
        }

        public int GFile_ParentArea(int area)
        {
            int i = -1;
            try
            {
                GFileArea gfa = _bbsDataContext.GFileAreas.FirstOrDefault(p => p.Id.Equals(area));
                if (gfa != null)
                {
                    i = gfa.ParentAreaId==null?-1:(int)gfa.ParentAreaId;
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GFile_ParentArea: " + e.Message);
            }
            return i;
        }

        public List<AreaListRow> GFileListArea(int? area, int userid)
        {
            //List all files and areas in the current area
            var response = new List<AreaListRow>();
            try
            {

                //User u = GetUserById(userid);
                //User must have at least one access group that matches the gfile area
                List<GFileArea> gfileAreas = _bbsDataContext.GFileAreas.Where(p => p.ParentAreaId==area ).ToList();
                List<GFileDetail> gfileDetails = _bbsDataContext.GFileDetails.Where(p => p.GFileAreaId==area).ToList();
                int listId = 1;
                foreach (GFileArea gfileArea in gfileAreas)
                {
                    listId++;
                    response.Add(new AreaListRow(gfileArea,listId,AreaListRowType.Area));
                }
                foreach (GFileDetail gfileDetail in gfileDetails)
                {

                    listId++;
                    response.Add(new AreaListRow(gfileDetail,listId,AreaListRowType.Entry));
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GFile_List_Area(" + area.ToString() + ") : " + e.Message);
            }
            return response;
        }

        public bool AddGFileDetail(int areaId, string filePath, string fileName,string title, string description, string notes, bool PETSCII)
        {
            //filename must include relative path from bbs
            //determines file size on its own
            //Will not allow re-add of the same filename to the same area
            bool b = false;
            try
            {
                if (!GFileExistsInArea(areaId, fileName))
                {
                    if (File.Exists(filePath+fileName))
                    {
                        GFileDetail gfd = new GFileDetail()
                        {
                            GFileAreaId = areaId,
                            Filename = fileName,
                            FilePath = filePath,
                            Added = DateTime.Now,
                            Title = title,
                            Description = description,
                            Notes = notes,
                            PETSCII = PETSCII,
                            FileSizeInBytes = (int)(new FileInfo(filePath+fileName).Length)
                        };
                        _bbsDataContext.GFileDetails.Add(gfd);
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
