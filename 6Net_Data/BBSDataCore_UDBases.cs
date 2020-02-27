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
        public List<UDFile> ListFilesForUDBase(int udbase)
        {
            try
            {
                if (_bbsDataContext.UDFiles.Count(p => p.UDBaseId.Equals(udbase)) > 0)
                {
                    return _bbsDataContext.UDFiles.Where(p => p.UDBaseId.Equals(udbase)).ToList();
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ListFilesForUDBase(" + udbase.ToString() + "): " + e.ToString());
            }
            return new List<UDFile>();
        }

        public void UploadedFile(UDFile udf)
        {
            try
            {
                _bbsDataContext.UDFiles.Add(udf);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.UploadedFile(" + udf.Filename + ", " + udf.Description + "): " + e.ToString());
            }

        }

        public IdAndKeys UDBase_ParentArea(int area)
        {
            IdAndKeys idak = new IdAndKeys()
            {
                Id = -1
            };
            idak.Keys.Add("title", "Main");
            try
            {

                UDBaseArea gfa = _bbsDataContext.UDBaseAreas.FirstOrDefault(p => p.Id.Equals(area));
                if (gfa != null)
                {
                    idak.Id = gfa.ParentAreaId;
                    if (gfa.ParentAreaId == -1)
                    {
                        idak.Keys["title"] = "Main";
                    }
                    else
                    {
                        idak.Keys["title"] = gfa.Title;
                    }
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.UDBase_ParentArea: " + e.Message);
            }
            return idak;
        }

        public List<IdAndKeys> UDBase_List_Area(int? area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                User u = _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userid));
                //TODO: Access group system
                //AccessGroup ag = _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<UDBaseArea> arealist = _bbsDataContext.UDBaseAreas.Where(p => p.ParentAreaId.Equals(area) 
                //&& p.AccessLevel <= ag.AccessGroupNumber
                ).ToList();
                List<UDBase> baselist = _bbsDataContext.UDBases.Where(p => p.UDBaseAreaId.Equals(area) 
                //&& p.AccessLevel <= ag.AccessGroupNumber
                ).ToList();
                //List<UDFile> filelist = bbs.UDFiles.Where(p=>p.UDBaseId.Equals(
                int listId = 1;
                foreach (UDBaseArea gfa in arealist)
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
                foreach (UDBase gfd in baselist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.Id
                    };
                    idak.Keys.Add("type", "base");
                    idak.Keys.Add("title", gfd.Title);
                    idak.Keys.Add("desc", gfd.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    idak.Keys.Add("filepath", gfd.FilePath);
                    listId++;
                    response.Add(idak);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.UDBase_List_Area(" + area.ToString() + ") : " + e.Message);
            }
            return response;
        }

    }
}
