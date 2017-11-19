using System;
using System.Collections.Generic;
using System.Linq;
//using SixNet_BBS.Data.Objects;
//using SixNet_BBS.BBS_Classes;
//using SixNet_BBS.Data.ReturnObjects;
using System.IO;
//using SixNet_BBS.Objects;
using SixNet_Logger;
using SixNet_StringUtils;

namespace SixNet_BBS_Data
{
    public class DataInterface
    {
        private readonly string _connectionString;

        public DataInterface(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int BBSDataDataContext;
        //{
        //    return BBSDataContext.GetContext();
        //}

        public BBSDataDataContext GetDataContext()
        {
            return new BBSDataDataContext(_connectionString);
        }

        public BBSConfig GetBBSConfig()
        {
            return GetDataContext().BBSConfigs.First(p => true);
        }

        #region GFiles
        public int GFile_ParentArea(int area)
        {
            int i = -1;
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                GFileArea gfa = bbs.GFileAreas.FirstOrDefault(p => p.GFileAreaId.Equals(area));
                if (gfa != null)
                {
                    i = gfa.ParentAreaId;
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GFile_ParentArea: " + e.Message);
            }
            return i;
        }

        public List<IdAndKeys> GFile_List_Area(int area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                User u = GetUserById(userid);
                AccessGroup ag = GetAccessGroup(u.AccessLevel);
                List<GFileArea> arealist = bbs.GFileAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                List<GFileDetail> filelist = bbs.GFileDetails.Where(p => p.GFileAreaId.Equals(area) ).ToList();
                int listId = 1;
                foreach (GFileArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfa.GFileAreaId
                    };
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (GFileDetail gfd in filelist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.GFileDetailId
                    };
                    idak.Keys.Add("type", "file");
                    idak.Keys.Add("title", gfd.DisplayFilename);
                    idak.Keys.Add("filename", gfd.Filename);
                    idak.Keys.Add("desc", gfd.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    idak.Keys.Add("gfiledetailid", gfd.GFileDetailId.ToString());
                    idak.Keys.Add("petscii", gfd.PETSCII.ToString());
                    listId++;
                    response.Add(idak);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GFile_List_Area(" + area.ToString() + ") : " + e.Message);
            }
            return response;
        }

        public bool AddGFile(int areaid, string title, string filename, string description, bool PETSCII)
        {
            //filename must include relative path from bbs
            //determines file size on its own
            //Will not allow re-add of the same filename to the same area
            bool b = false;
            try
            {
                if (!GFileExistsInArea(areaid, filename))
                {
                    if (File.Exists(filename))
                    {
                        GFileDetail gfd = new GFileDetail()
                        {
                            GFileAreaId = areaid,
                            Filename = filename,
                            Added = DateTime.Now,
                            DisplayFilename = title,
                            Description = description,
                            PETSCII = PETSCII,
                            FileSizeInBytes = (int)(new FileInfo(filename).Length)
                        };
                        BBSDataDataContext bbs = GetDataContext();
                        bbs.GFileDetails.InsertOnSubmit(gfd);
                        bbs.SubmitChanges();
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
            BBSDataDataContext bbs = GetDataContext();
            int x = bbs.GFileDetails.Count(p => p.GFileAreaId.Equals(areaid) && p.Filename.Equals(filename));
            return (x > 0) ;
        }

        public GFileDetail GetGFileDetail(int id)
        {
            BBSDataDataContext bbs = GetDataContext();
            return bbs.GFileDetails.FirstOrDefault(p => p.GFileDetailId.Equals(id));
        }

        #endregion

        #region PFiles
        public List<PFileDetail> ListPFilesForAreas(int area)
        {
            List<PFileDetail> pfilelist = new List<PFileDetail>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                pfilelist = bbs.PFileDetails.Where(p => p.ParentAreaId.Equals(area)).ToList();
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
                BBSDataDataContext bbs = GetDataContext();
                pf = bbs.PFileDetails.FirstOrDefault(p => p.ParentAreaId.Equals(area) && p.PFileNumber.Equals(number));
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
                BBSDataDataContext bbs = GetDataContext();
                PFileArea gfa = bbs.PFileAreas.FirstOrDefault(p => p.PFileAreaId.Equals(area));
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
                BBSDataDataContext bbs = GetDataContext();
                List<PFileArea> arealist = bbs.PFileAreas.Where(p => p.ParentAreaId.Equals(area)).ToList();
                List<PFileDetail> filelist = bbs.PFileDetails.Where(p => p.ParentAreaId.Equals(area)).ToList();
                int listId = 1;
                foreach (PFileArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfa.PFileAreaId
                    };
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (PFileDetail gfd in filelist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.PFileDetailId
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
        #endregion

        #region MessageBase

        public string MessageBaseAreaName(int areaid)
        {
            try
            {
                return GetDataContext().MessageBaseAreas.FirstOrDefault(p => p.MessageBaseAreaId.Equals(areaid)).Title;
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageBaseAreaName: " + e.Message);
                return "Invalid";
            }
        }

        public void SaveBBSConfig(BBSConfig bbsConfig)
        {
            var dataContext = GetDataContext();
            var bc = dataContext.BBSConfigs.First(p => true);
            bc.BBS_Name = bbsConfig.BBS_Name;
            bc.BBS_Port = bbsConfig.BBS_Port;
            bc.BBS_URL = bbsConfig.BBS_URL;
            bc.SysopMenuPass = bbsConfig.SysopMenuPass;
            bc.SysOp_Email = bbsConfig.SysOp_Email;
            bc.SysOp_Handle = bbsConfig.SysOp_Handle;
            dataContext.SubmitChanges();
        }

        public IdAndKeys MessageBase_ParentArea(int area)
        {
            IdAndKeys idak = new IdAndKeys()
            {
                Id = -1
            };
            idak.Keys.Add("title", "Main");
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                MessageBaseArea gfa = bbs.MessageBaseAreas.FirstOrDefault(p => p.MessageBaseAreaId.Equals(area));
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
                LoggingAPI.LogEntry("Exception in DataInterface.MessageBase_ParentArea: " + e.Message);
            }
            return idak;
        }


        public List<IdAndKeys> MessageBase_List_Area(int area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                User u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
                AccessGroup ag = bbs.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<MessageBaseArea> arealist = bbs.MessageBaseAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                List<MessageBase> baselist = bbs.MessageBases.Where(p => p.ParentArea.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                int listId = 1;
                foreach (MessageBaseArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfa.MessageBaseAreaId
                    };
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (MessageBase gfd in baselist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.MessageBaseId
                    };
                    idak.Keys.Add("type", "file");
                    idak.Keys.Add("title", gfd.Title);
                    idak.Keys.Add("desc", gfd.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageBase_List_Area(" + area.ToString() + ") : " + e.Message);
            }
            return response;
        }

        public void PostReply(int messagebase, string subject, bool anon, int userid, string message, int threadid)
        {
            try
            {
                BBSDataDataContext bbs = GetDataContext();

                MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId =threadid };
                bbs.MessageHeaders.InsertOnSubmit(header);
                bbs.SubmitChanges();

                MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.MessageHeaderId };
                bbs.MessageBodies.InsertOnSubmit(body);

                bbs.SubmitChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }
        //Start new thread
        public void PostMessage(int messagebase, string subject, bool anon, int userid, string message)
        {
            try
            {
                BBSDataDataContext bbs = GetDataContext();

                MessageThread thread = new MessageThread() { MessageBaseId = messagebase, InitialMessageHeaderId = -1 };
                bbs.MessageThreads.InsertOnSubmit(thread);
                bbs.SubmitChanges();

                MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId = thread.MessageThreadId };
                bbs.MessageHeaders.InsertOnSubmit(header);
                bbs.SubmitChanges();

                thread.InitialMessageHeaderId = header.MessageHeaderId;
                bbs.SubmitChanges();

                MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.MessageHeaderId };
                bbs.MessageBodies.InsertOnSubmit(body);

                bbs.SubmitChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }

        public List<ThreadListRow> ListThreadsForBase(int messagebase)
        {
            List<ThreadListRow> threads = new List<ThreadListRow>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                string query = "SELECT MT.MessageThreadId AS MessageThreadId, "
                               + " IM.Subject AS Subject, " 
                               + " CASE WHEN LM.Posted IS NULL THEN"
                               + " 	CASE WHEN IM.Posted IS NULL THEN"
                               + " 		'1970-01-01 00:00:00' "
                               + " 	ELSE IM.Posted END "
                               + " ELSE LM.Posted END AS LastActivity, "
                               + " U.Username AS Poster, "
                               + " CASE WHEN U.UserId IS NULL THEN -1 ELSE U.UserId END As PosterId,  "
                               + " COUNT(RC.MessageHeaderId) AS Replies FROM MessageThreads "
                               + " MT LEFT JOIN MessageHeaders IM ON (MT.InitialMessageHeaderId = IM.MessageHeaderId) "
                               + " LEFT JOIN MessageHeaders LM ON (MT.MessageThreadId = LM.MessageThreadId) "
                               + " LEFT JOIN MessageHeaders RC ON (MT.MessageThreadId = RC.MessageThreadId AND MT.InitialMessageHeaderId != RC.MessageThreadId) "
                               + " LEFT OUTER JOIN MessageHeaders LD ON (LD.MessageThreadId = MT.MessageThreadId AND (LM.Posted < LD.Posted OR LM.Posted = LD.Posted AND LM.MessageHeaderId < LD.MessageHeaderId)) "
                               + " LEFT JOIN Users U ON (LM.UserId = U.UserId) "
                               + " WHERE LD.MessageHeaderId IS NULL AND MT.MessageBaseId = " + messagebase.ToString()
                               + " GROUP BY MT.MessageThreadId,IM.Subject,IM.Posted,LM.Posted,U.Username, U.Userid"
                   ;

                threads = bbs.ExecuteQuery<ThreadListRow>(query).ToList<ThreadListRow>();
                //int i = 1;
                //foreach (ThreadListRow threadinfo in threads)
                //{
                //    IdAndKeys idak = new IdAndKeys();
                //    idak.Id = threadinfo.MessageThreadId;
                //    idak.Keys.Add("subject", threadinfo.Subject);
                //    idak.Keys.Add("activity", threadinfo.LastActivity.ToString("yy-MM-dd hh:mm"));
                //    idak.Keys.Add("poster", threadinfo.Poster);
                //    idak.Keys.Add("posterid", threadinfo.PosterId.ToString());
                //    idak.Keys.Add("listid", i.ToString());
                //    i++;
                //    result.Add(idak);
                //}
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ListThreadsForBase(" + messagebase.ToString() + "): " + e.ToString());
            }
            return threads;
        }


        public void MarkRead(int userid, int messageid)
        {
            try
            {
                if (!HasRead(userid, messageid))
                {
                    BBSDataDataContext bbs = GetDataContext();
                    UserRead ur = new UserRead() { UserId = userid, MessageHeaderId = messageid };
                    bbs.UserReads.InsertOnSubmit(ur);
                    bbs.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MarkRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            }
        }

        public bool HasRead(int userid, int messageid)
        {
            bool b = false;
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                b = bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(messageid);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.HasRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            }
            return b;
        }

        public int FirstUnread(int userid, List<int> messageids)
        {
            int i = -1;
            if (messageids != null)
            {
                if (messageids.Count > 0)
                {
                    try
                    {
                        BBSDataDataContext bbs = GetDataContext();
                        i = messageids.FirstOrDefault(q => !(
                            bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(q))
                            );
                     //   if (i == null) i = -1;
                    }
                    catch (Exception e)
                    {
                        LoggingAPI.LogEntry("Exception in DataInterface.FirstUnread(" + userid.ToString() + ",LIST):" + e.ToString());
                    }
                }
            }
            return i;
        }

        public List<int> MessageIdsInThread(int threadid)
        {
            List<int> result = new List<int>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                String Query = "SELECT MessageHeaderId FROM MessageHeaders WHERE MessageThreadId = " + threadid.ToString() + "ORDER BY Posted ";

                result = bbs.ExecuteQuery<int>(Query).ToList<int>();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageIdsInThread(" + threadid.ToString() + "):" + e.ToString());
            }
            return result;

        }

        public BBS_Message GetMessage(int MessageId)
        {
            BBS_Message bm = new BBS_Message();
            BBSDataDataContext bbs = GetDataContext();
            bm.Header = bbs.MessageHeaders.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
            bm.Body = bbs.MessageBodies.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
            return bm;
        }

        #endregion

        #region Users

        public User Login(string un, string pw)
        {
            User u = null;
            try
            {
                string username = Utils.ToSQL(un);
                string password = Utils.ToSQL(pw);
                BBSDataDataContext bbs = GetDataContext();
                u = bbs.Users.FirstOrDefault(p => p.Username.ToUpper().Equals(username.ToUpper()) && p.HashedPassword.Equals(password));
                u.Username = Utils.FromSQL(u.Username);
                u.HashedPassword = Utils.FromSQL(u.HashedPassword);
                u.RealName = Utils.FromSQL(u.RealName);
                u.Email = Utils.FromSQL(u.Email);
                u.ComputerType = Utils.FromSQL(u.ComputerType);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.Login: " + e.ToString());
                u = null;
            }
            return u;
        }

        public bool ValidNewUsername(string s)
        {
            bool b = false;
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                string uname = Utils.ToSQL(s);
                b = (bbs.Users.Count(p => p.Username.ToUpper().Equals(uname.ToUpper())) == 0);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ValidNewUsername: " + e.ToString());
                b = false;
            }

            return b;
        }

        public bool SaveNewUser(string un, string pw, string rn, string em, string co)
        {
            bool b = false;
            try
            {
                string Un = Utils.ToSQL(un);
                string Pw = Utils.ToSQL(pw);
                string Rn = Utils.ToSQL(rn);
                string Em = Utils.ToSQL(em);
                string Co = Utils.ToSQL(co);
                User u = new User() { Username = Un, HashedPassword = Pw, RealName = Rn, Email = Em, ComputerType = Co, AccessLevel = 0 };
                u.LastConnection = DateTime.Now;
                u.LastDisconnection = DateTime.Now;
                BBSDataDataContext bbs = GetDataContext();
                bbs.Users.InsertOnSubmit(u);
                bbs.SubmitChanges();
                b = true;

            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.SaveNewUser: " + e.ToString());
                b = false;
            }
            return b;
        }

        public User GetUserById(int userid)
        {
            User u = null;
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetUserById: " + e);
            }
            return u;
        }
        #endregion

        #region Access Groups
        public AccessGroup GetAccessGroup(int level)
        {
            BBSDataDataContext bbs = GetDataContext();
            return bbs.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(level));
        }

        public List<AccessGroup> ListAccessGroups()
        {
            BBSDataDataContext bbs = GetDataContext();
            return bbs.AccessGroups.ToList();
        }

        #endregion

        public List<NewsRow> GetNews(DateTime fromdate)
        {
            BBSDataDataContext bbs = GetDataContext();
            List<NewsRow> newsrows = new List<NewsRow>();
            List<News_Item> news = bbs.News_Items.Where(p => p.Posted > fromdate).OrderBy(p => p.Posted).ToList();
            foreach (News_Item ni in news)
            {
                newsrows.Add(new NewsRow() { Body = ni.Body, Posted = ni.Posted, Subject = ni.Subject });
            }
            return newsrows;
        }

        public void NewFeedback(string subject, string body, int fromuser)
        {
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                Feedback f = new Feedback() { Subject = subject, Body = body, FromUser = fromuser, Sent = DateTime.Now };
                bbs.Feedbacks.InsertOnSubmit(f);
                bbs.SubmitChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.NewFeedback: " + e.ToString());
            }
        }


        public void AddGraffiti(string graffiti, int userid)
        {
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                Graffiti g = new Graffiti() { Content = graffiti, Posted = DateTime.Now, UserId = userid };
                bbs.Graffitis.InsertOnSubmit(g);
                bbs.SubmitChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.AddGraffiti: " + e.ToString());
            }
        }



        public List<Dictionary<string, string>> GetGraffiti()
        {
            List<Dictionary<string, string>> glist = new List<Dictionary<string, string>>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                var gl2 = bbs.Graffitis.OrderByDescending(p => p.Posted).Take(10)
                    .Join(bbs.Users, g => g.UserId, u => u.UserId, (g, u) => new { graf = g.Content, posted = g.Posted, user = u.Username })
                    .ToList();

                foreach (var gl in gl2)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>
                    {
                        { "graf", gl.graf },
                        { "user", gl.user }
                    };
                    glist.Add(d);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetGraffiti: " + e.ToString());
            }
            return glist;
        }

        public List<Dictionary<string, string>> GetLast10()
        {
            List<Dictionary<string, string>> glist = new List<Dictionary<string, string>>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                var gl2 = bbs.CallLogs.OrderByDescending(p => p.Connected).Take(10)
                    .Join(bbs.Users, g => g.UserId, u => u.UserId, (g, u) => new { connected = g.Connected,  user = u.Username })
                    .ToList();

                foreach (var gl in gl2)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>
                    {
                        { "when", gl.connected.ToString("yyyy-MM-dd hh:mm") },
                        { "user", gl.user }
                    };
                    glist.Add(d);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetLast10: " + e.ToString());
            }
            return glist;
        }

        public int RecordConnection(int userid)
        {
            int i = -1;
            BBSDataDataContext bbs = GetDataContext();
            CallLog cl = new CallLog() { Connected = DateTime.Now, Disconnected = DateTime.Now, UserId = userid };
            bbs.CallLogs.InsertOnSubmit(cl);
            bbs.SubmitChanges();
            return i;
        }

        public void UpdateCallLog(int callLogId, int userid) //Used after new user reg
        {
            BBSDataDataContext bbs = GetDataContext();
            CallLog cl = bbs.CallLogs.FirstOrDefault(p => p.CallLogId.Equals(callLogId));
            if (cl != null)
            {
                cl.UserId = userid;
                bbs.SubmitChanges();
            }
        }
        public void RecordDisconnection(int callLogId)
        {
            BBSDataDataContext bbs = GetDataContext();
            CallLog cl = bbs.CallLogs.FirstOrDefault(p => p.CallLogId.Equals(callLogId));
            if (cl != null)
            {
                cl.Disconnected = DateTime.Now;
                bbs.SubmitChanges();
            }
        }

        public List<IdAndKeys> GetAllUserDefinedFieldsWithKey(string key)
        {
            BBSDataDataContext bbs = GetDataContext();
            List<IdAndKeys> idklist = new List<IdAndKeys>();
            List<UserDefinedField> udflist = bbs.UserDefinedFields.Where(p => p.Key.ToUpper().Equals(key.ToUpper())).ToList();
            if (udflist != null)
            {
                if (udflist.Count > 0)
                {
                    foreach (UserDefinedField udf in udflist)
                    {
                        IdAndKeys idk = new IdAndKeys()
                        {
                            Id = udf.UserId
                        };
                        idk.Keys.Add("data", udf.FieldValue);
                        idk.Keys.Add("key", udf.Key);
                        idk.Keys.Add("recid", udf.UserDefinedFieldId.ToString());
                        idk.Keys.Add("userid", udf.UserId.ToString());
                        idklist.Add(idk);
                    }
                }
            }
            return idklist;
        }

        public string GetUserDefinedField(int userid, string key)
        {
            BBSDataDataContext bbs = GetDataContext();
            UserDefinedField udf = bbs.UserDefinedFields.FirstOrDefault(p => p.UserId.Equals(userid) && p.Key.ToUpper().Equals(key.ToUpper()));
            if (udf != null)
            {
                return udf.FieldValue;
            }
            else
            {
                return "";
            }

        }

        public void SaveUserDefinedField(int userid, string key, string value)
        {
            BBSDataDataContext bbs = GetDataContext();
            UserDefinedField udf = bbs.UserDefinedFields.FirstOrDefault(p => p.UserId.Equals(userid) && p.Key.ToUpper().Equals(key.ToUpper()));
            if (udf != null)
            {
                //Modify
                udf.FieldValue = value;
            }
            else
            {
                //New
                udf = new UserDefinedField() { Key = key, FieldValue = value, UserId = userid };
                bbs.UserDefinedFields.InsertOnSubmit(udf);
            }
            bbs.SubmitChanges();

        }

        public void AppendUserDefinedField(int userid, string key, string value)
        {
            String s = GetUserDefinedField(userid, key);
            s = s + value;
            SaveUserDefinedField(userid, key, s);
        }


        public User GetUser(int userid)
        {
            BBSDataDataContext bbs = GetDataContext();
            User u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
            return u;
        }



        #region UDBases
        public List<UDFile> ListFilesForUDBase(int udbase)
        {
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                if (bbs.UDFiles.Count(p => p.UDBaseId.Equals(udbase)) > 0)
                {
                    return bbs.UDFiles.Where(p => p.UDBaseId.Equals(udbase)).ToList();
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
                BBSDataDataContext bbs = GetDataContext();
                bbs.UDFiles.InsertOnSubmit(udf);
                bbs.SubmitChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.UploadedFile(" + udf.Filename + ", " + udf.Description + "): "+e.ToString());
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
                BBSDataDataContext bbs = GetDataContext();
                UDBaseArea gfa = bbs.UDBaseAreas.FirstOrDefault(p => p.UDBaseAreaId.Equals(area));
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

        public List<IdAndKeys> UDBase_List_Area(int area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataDataContext bbs = GetDataContext();
                User u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
                AccessGroup ag = bbs.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<UDBaseArea> arealist = bbs.UDBaseAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                List<UDBase> baselist = bbs.UDBases.Where(p => p.ParentArea.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                //List<UDFile> filelist = bbs.UDFiles.Where(p=>p.UDBaseId.Equals(
                int listId = 1;
                foreach (UDBaseArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfa.UDBaseAreaId
                    };
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (UDBase gfd in baselist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.UDBaseId
                    };
                    idak.Keys.Add("type", "base");
                    idak.Keys.Add("title", gfd.Title);
                    idak.Keys.Add("desc", gfd.LongDescription);
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


        
        #endregion

        #region Pass-thrus
        public List<MessageBaseArea> MessageBaseAreas()
        {
            return GetDataContext().MessageBaseAreas.OrderBy(p=>p.MessageBaseAreaId).ToList();
        }

        public List<MessageBase> MessageBases()
        {
            return GetDataContext().MessageBases.OrderBy(p=>p.MessageBaseId).ToList();
        }

        public List<CallLog> CallLogs()
        {
            return GetDataContext().CallLogs.OrderByDescending(p => p.CallLogId).ToList();
        }

        public List<AccessGroup> AccessGroups()
        {
            return GetDataContext().AccessGroups.OrderBy(p => p.AccessGroupNumber).ToList();
        }

        public List<User> Users()
        {
            return GetDataContext().Users.OrderBy(p => p.UserId).ToList();
        }


        #endregion
    }
}
