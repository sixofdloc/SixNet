using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Classes;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public List<PFileDetail> ListPFilesForAreas(int area)
        {
            List<PFileDetail> pfilelist = new List<PFileDetail>();
            try
            {

                pfilelist = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId.Equals(area)).ToList();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ListPFilesForAreas: " + e.Message);
                pfilelist = new List<PFileDetail>();
            }

            return pfilelist;

        }

        public PFileDetail GetPFileDetailByAreaAndNumber(int area, int number)
        {
            PFileDetail pf = null;
            try
            {
                pf = _bbsDataContext.PFileDetails.FirstOrDefault(p => p.PFileAreaId.Equals(area) //& p.PFileNumber.Equals(number)
                );
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetPFileDetailByAreaAndNumber: " + e.Message);
                pf = null;
            }
            return pf;
        }
        public int PFile_ParentArea(int area)
        {
            int i = -1;
            try
            {

                PFileArea gfa = _bbsDataContext.PFileAreas.FirstOrDefault(p => p.ParentAreaId.Equals(area));
                if (gfa != null)
                {
                    i = gfa.ParentAreaId;
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PFile_ParentArea: " + e.Message);
            }
            return i;
        }

        public List<IdAndKeys> PFile_List_Area(int area)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {

                List<PFileArea> arealist = _bbsDataContext.PFileAreas.Where(p => p.ParentAreaId.Equals(area)).ToList();
                List<PFileDetail> filelist = _bbsDataContext.PFileDetails.Where(p => p.PFileAreaId.Equals(area)).ToList();
                int listId = 1;
                foreach (PFileArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfa.Id
                    };
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (PFileDetail gfd in filelist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.Id
                    };
                    idak.Keys.Add("type", "file");
                    idak.Keys.Add("title", gfd.Filename);
                    idak.Keys.Add("desc", gfd.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PFile_List_Area(" + area.ToString() + ") : " + e.Message);
            }
            return response;
        }

    }
}
