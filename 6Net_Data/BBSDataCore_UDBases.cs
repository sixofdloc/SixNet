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
        public List<UDFile> ListFilesForUDBase(int udBaseId)
        {
            try
            {
                if (_bbsDataContext.UDFiles.Count(p => p.UDBaseId.Equals(udBaseId)) > 0)
                {
                    return _bbsDataContext.UDFiles.Where(p => p.UDBaseId.Equals(udBaseId)).ToList();
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { udBaseId }); 
            }
            return new List<UDFile>();
        }

        public void UploadedFile(UDFile udFile)
        {
            try
            {
                _bbsDataContext.UDFiles.Add(udFile);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { udFile }); 
            }

        }
        //TODO: return object instead of IDAK?
        public IdAndKeys UDBase_ParentArea(int udBaseId)
        {
            IdAndKeys parentArea = new IdAndKeys()
            {
                Id = -1
            };
            parentArea.Keys.Add("title", "Main");
            try
            {

                UDBaseArea udBaseArea = _bbsDataContext.UDBaseAreas.FirstOrDefault(p => p.Id.Equals(udBaseId));
                if (udBaseArea != null)
                {
                    parentArea.Id = udBaseArea.ParentAreaId;
                    if (udBaseArea.ParentAreaId == -1)
                    {
                        parentArea.Keys["title"] = "Main";
                    }
                    else
                    {
                        parentArea.Keys["title"] = udBaseArea.Title;
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { udBaseId, parentArea }); 
            }
            return parentArea;
        }

        public List<IdAndKeys> UDBase_List_Area(int? areaId, int userId)
        {
            List<IdAndKeys> listForThisArea = new List<IdAndKeys>();
            try
            {
                User user = _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userId));
                //TODO: Access group system
                //AccessGroup ag = _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<UDBaseArea> areaList = _bbsDataContext.UDBaseAreas.Where(p => p.ParentAreaId.Equals(areaId) 
                //&& p.AccessLevel <= ag.AccessGroupNumber
                ).ToList();
                List<UDBase> baseList = _bbsDataContext.UDBases.Where(p => p.UDBaseAreaId.Equals(areaId) 
                //&& p.AccessLevel <= ag.AccessGroupNumber
                ).ToList();
                //List<UDFile> filelist = bbs.UDFiles.Where(p=>p.UDBaseId.Equals(
                int listId = 1;
                foreach (UDBaseArea udBaseArea in areaList)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = udBaseArea.Id
                    };
                    idak.Keys.Add("title", udBaseArea.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", udBaseArea.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    listForThisArea.Add(idak);
                }
                foreach (UDBase udBase in baseList)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = udBase.Id
                    };
                    idak.Keys.Add("type", "base");
                    idak.Keys.Add("title", udBase.Title);
                    idak.Keys.Add("desc", udBase.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    idak.Keys.Add("filepath", udBase.FilePath);
                    listId++;
                    listForThisArea.Add(idak);
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId,userId, listForThisArea }); 
            }
            return listForThisArea;
        }

    }
}
