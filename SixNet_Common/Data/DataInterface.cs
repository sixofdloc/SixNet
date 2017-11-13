using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SixNet_BBS.Data.Objects;
using SixNet_BBS.BBS_Classes;
using SixNet_BBS.Data.ReturnObjects;
using System.IO;
using SixNet_BBS.Objects;
using SixNet_Logger;

namespace SixNet_BBS.Data
{
    public class DataInterface
    {
        public static BBSDataContext GetContext()
        {
            return BBSDataContext.GetContext();
        }



        #region GFiles
        public static int GFile_ParentArea(int area)
        {
            int i = -1;
            try
            {
                BBSDataContext bbs = new BBSDataContext();
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

        public static List<IdAndKeys> GFile_List_Area(int area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataContext bbs = new BBSDataContext();
                User u = GetUserById(userid);
                AccessGroup ag = GetAccessGroup(u.AccessLevel);
                List<GFileArea> arealist = bbs.GFileAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                List<GFileDetail> filelist = bbs.GFileDetails.Where(p => p.GFileAreaId.Equals(area) ).ToList();
                int listId = 1;
                foreach (GFileArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfa.GFileAreaId;
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (GFileDetail gfd in filelist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfd.GFileDetailId;
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

        public static bool AddGFile(int areaid, string title, string filename, string description, bool PETSCII)
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
                        GFileDetail gfd = new GFileDetail();
                        gfd.GFileAreaId = areaid;
                        gfd.Filename = filename;
                        gfd.Added = DateTime.Now;
                        gfd.DisplayFilename = title;
                        gfd.Description = description;
                        gfd.PETSCII = PETSCII;
                        gfd.FileSizeInBytes = (int)(new FileInfo(filename).Length);
                        BBSDataContext bbs = new BBSDataContext();
                        bbs.GFileDetails.Add(gfd);
                        bbs
                        b = true;
                    }
                }
            }
            catch (Exception e)
            {
                b = false;
            }
            return b;
        }
        public static bool GFileExistsInArea(int areaid, string filename)
        {
            BBSDataContext bbs = new BBSDataContext();
            int x = bbs.GFileDetails.Count(p => p.GFileAreaId.Equals(areaid) && p.Filename.Equals(filename));
            return (x > 0) ;
        }

        public static GFileDetail GetGFileDetail(int id)
        {
            BBSDataContext bbs = new BBSDataContext();
            return bbs.GFileDetails.FirstOrDefault(p => p.GFileDetailId.Equals(id));
        }

        #endregion

        #region PFiles
        public static List<PFileDetail> ListPFilesForAreas(int area)
        {
            List<PFileDetail> pfilelist = new List<PFileDetail>();
            try
            {
                BBSDataContext bbs = GetContext();
                pfilelist = bbs.PFileDetails.Where(p => p.ParentAreaId.Equals(area)).ToList();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ListPFilesForAreas: " + e.Message);
                pfilelist = new List<PFileDetail>();
            }

            return pfilelist;

        }

        public static PFileDetail GetPFileDetailByAreaAndNumber(int area, int number)
        {
            PFileDetail pf = null;
            try
            {
                BBSDataContext bbs = GetContext();
                pf = bbs.PFileDetails.FirstOrDefault(p => p.ParentAreaId.Equals(area) && p.PFileNumber.Equals(number));
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetPFileDetailByAreaAndNumber: " + e.Message);
                pf = null;
            }
            return pf;
        }
        public static int PFile_ParentArea(int area)
        {
            int i = -1;
            try
            {
                BBSDataContext bbs = new BBSDataContext();
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

        public static List<IdAndKeys> PFile_List_Area(int area)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataContext bbs = new BBSDataContext();
                List<PFileArea> arealist = bbs.PFileAreas.Where(p => p.ParentAreaId.Equals(area)).ToList();
                List<PFileDetail> filelist = bbs.PFileDetails.Where(p => p.ParentAreaId.Equals(area)).ToList();
                int listId = 1;
                foreach (PFileArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfa.PFileAreaId;
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (PFileDetail gfd in filelist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfd.PFileDetailId;
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

        public static IdAndKeys MessageBase_ParentArea(int area)
        {
            IdAndKeys idak = new IdAndKeys();
            idak.Id = -1;
            idak.Keys.Add("title", "Main");
            try
            {
                BBSDataContext bbs = new BBSDataContext();
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

        public static List<IdAndKeys> MessageBase_List_Area(int area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                BBSDataContext bbs = new BBSDataContext();
                User u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
                AccessGroup ag = bbs.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<MessageBaseArea> arealist = bbs.MessageBaseAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                List<MessageBase> baselist = bbs.MessageBases.Where(p => p.ParentArea.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
                int listId = 1;
                foreach (MessageBaseArea gfa in arealist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfa.MessageBaseAreaId;
                    idak.Keys.Add("title", gfa.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", gfa.LongDescription);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    response.Add(idak);
                }
                foreach (MessageBase gfd in baselist)
                {
                    IdAndKeys idak = new IdAndKeys();
                    idak.Id = gfd.MessageBaseId;
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

        public static void PostReply(int messagebase, string subject, bool anon, int userid, string message, int threadid)
        {
            try
            {
                BBSDataContext bbs = GetContext();

                MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId =threadid };
                bbs.MessageHeaders.Add(header);
                bbs.SaveChanges();

                MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.MessageHeaderId };
                bbs.MessageBodies.Add(body);

                bbs.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }
        //Start new thread
        public static void PostMessage(int messagebase, string subject, bool anon, int userid, string message)
        {
            try
            {
                BBSDataContext bbs = GetContext();

                MessageThread thread = new MessageThread() { MessageBaseId = messagebase, InitialMessageHeaderId = -1 };
                bbs.MessageThreads.Add(thread);
                bbs.SaveChanges();

                MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId = thread.MessageThreadId };
                bbs.MessageHeaders.Add(header);
                bbs.SaveChanges();

                thread.InitialMessageHeaderId = header.MessageHeaderId;
                bbs.SaveChanges();

                MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.MessageHeaderId };
                bbs.MessageBodies.Add(body);

                bbs.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }

        public static List<ThreadListRow> ListThreadsForBase(int messagebase)
        {
            List<ThreadListRow> threads = new List<ThreadListRow>();
            try
            {
                BBSDataContext bbs = GetContext();
                string query = "SELECT MT.MessageThreadId AS MessageThreadId, "
                               + " IM.Subject AS Subject, " 
                               + " CASE WHEN LM.Posted IS NULL THEN"
                               + " 	CASE WHEN IM.Posted IS NULL THEN"
                               + " 		'1970-01-01 00:00:00' "
                               + " 	ELSE IM.Posted END "
                               + " ELSE LM.Posted END AS LastActivity, "
                               + " U.Username AS Poster, "
                               + " CASE WHEN U.UserId IS NULL THEN -1 ELSE U.UserId END As PosterId,  "
                               + " COUNT(RC.MessageHeaderId) AS Replies FROM MessageThreads MT LEFT JOIN MessageHeaders IM ON (MT.InitialMessageHeaderId = IM.MessageHeaderId) LEFT JOIN MessageHeaders LM ON (MT.MessageThreadId = LM.MessageThreadId) LEFT JOIN MessageHeaders RC ON (MT.MessageThreadId = RC.MessageThreadId AND MT.InitialMessageHeaderId != RC.MessageThreadId) LEFT OUTER JOIN MessageHeaders LD ON (LD.MessageThreadId = MT.MessageThreadId AND (LM.Posted < LD.Posted OR LM.Posted = LD.Posted AND LM.MessageHeaderId < LD.MessageHeaderId)) LEFT JOIN Users U ON (LM.UserId = U.UserId) WHERE LD.MessageHeaderId IS NULL GROUP BY MT.MessageThreadId,IM.Subject,IM.Posted,LM.Posted,U.Username, U.Userid"
                   ;

                threads = bbs.Database.SqlQuery<ThreadListRow>(query).ToList<ThreadListRow>();
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


        public static void MarkRead(int userid, int messageid)
        {
            try
            {
                if (!HasRead(userid, messageid))
                {
                    BBSDataContext bbs = GetContext();
                    UserRead ur = new UserRead() { UserId = userid, MessageHeaderId = messageid };
                    bbs.UserReads.Add(ur);
                    bbs.SaveChanges();
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MarkRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            }
        }

        public static bool HasRead(int userid, int messageid)
        {
            bool b = false;
            try
            {
                BBSDataContext bbs = GetContext();
                b = bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(messageid);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.HasRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            }
            return b;
        }

        public static int FirstUnread(int userid, List<int> messageids)
        {
            int i = -1;
            if (messageids != null)
            {
                if (messageids.Count > 0)
                {
                    try
                    {
                        BBSDataContext bbs = GetContext();
                        i = messageids.FirstOrDefault(q => !(
                            bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(q))
                            );
                        if (i == null) i = -1;
                    }
                    catch (Exception e)
                    {
                        LoggingAPI.LogEntry("Exception in DataInterface.FirstUnread(" + userid.ToString() + ",LIST):" + e.ToString());
                    }
                }
            }
            return i;
        }

        public static List<int> MessageIdsInThread(int threadid)
        {
            List<int> result = new List<int>();
            try
            {
                BBSDataContext bbs = GetContext();
                String Query = "SELECT MessageHeaderId FROM MessageHeaders WHERE MessageThreadId = " + threadid.ToString() + "ORDER BY Posted ";

                result = bbs.Database.SqlQuery<int>(Query).ToList<int>();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageIdsInThread(" + threadid.ToString() + "):" + e.ToString());
            }
            return result;

        }

        public static BBS_Message GetMessage(int MessageId)
        {
            BBS_Message bm = new BBS_Message();
            BBSDataContext bbs = new BBSDataContext();
            bm.Header = bbs.MessageHeaders.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
            bm.Body = bbs.MessageBodies.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
            return bm;
        }

        #endregion

        #region Users

        public static User Login(string un, string pw)
        {
            User u = null;
            try
            {
                string username = Utils.ToSQL(un);
                string password = Utils.ToSQL(pw);
                BBSDataContext bbs = GetContext();
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

        public static bool ValidNewUsername(string s)
        {
            bool b = false;
            try
            {
                BBSDataContext bbs = GetContext();
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

        public static bool SaveNewUser(string un, string pw, string rn, string em, string co)
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
                BBSDataContext bbs = GetContext();
                bbs.Users.Add(u);
                bbs.SaveChanges();
                b = true;

            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.SaveNewUser: " + e.ToString());
                b = false;
            }
            return b;
        }

        public static User GetUserById(int userid)
        {
            User u = null;
            try
            {
                BBSDataContext bbs = new BBSDataContext();
                u = bbs.Users.FirstOrDefault(p => p.UserId.Equals(userid));
            }
            catch (Exception e)
            {
            }
            return u;
        }
        #endregion

        #region Access Groups
        public static AccessGroup GetAccessGroup(int level)
        {
            BBSDataContext bbs = new BBSDataContext();
            return bbs.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(level));
        }

        public static List<AccessGroup> ListAccessGroups()
        {
            BBSDataContext bbs = new BBSDataContext();
            return bbs.AccessGroups.ToList();
        }

        #endregion

        public static List<NewsRow> GetNews(DateTime fromdate)
        {
            BBSDataContext bbs = new BBSDataContext();
            List<NewsRow> newsrows = new List<NewsRow>();
            List<News_Item> news = bbs.News.Where(p => p.Posted > fromdate).OrderBy(p => p.Posted).ToList();
            foreach (News_Item ni in news)
            {
                newsrows.Add(new NewsRow() { Body = ni.Body, Posted = ni.Posted, Subject = ni.Subject });
            }
            return newsrows;
        }

        public static void NewFeedback(string subject, string body, int fromuser)
        {
            try
            {
                BBSDataContext bbs = GetContext();
                Feedback f = new Feedback() { Subject = subject, Body = body, FromUser = fromuser, Sent = DateTime.Now };
                bbs.Feedbacks.Add(f);
                bbs.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.NewFeedback: " + e.ToString());
            }
        }


        public static void AddGraffiti(string graffiti, int userid)
        {
            try
            {
                BBSDataContext bbs = GetContext();
                Graffiti g = new Graffiti() { Content = graffiti, Posted = DateTime.Now, UserId = userid };
                bbs.Graffitis.Add(g);
                bbs.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.AddGraffiti: " + e.ToString());
            }
        }



        public static List<Dictionary<string, string>> GetGraffiti()
        {
            List<Dictionary<string, string>> glist = new List<Dictionary<string, string>>();
            try
            {
                BBSDataContext bbs = GetContext();
                var gl2 = bbs.Graffitis.OrderByDescending(p => p.Posted).Take(10)
                    .Join(bbs.Users, g => g.UserId, u => u.UserId, (g, u) => new { graf = g.Content, posted = g.Posted, user = u.Username })
                    .ToList();

                foreach (var gl in gl2)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    d.Add("graf", gl.graf);
                    d.Add("user", gl.user);
                    glist.Add(d);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetGraffiti: " + e.ToString());
            }
            return glist;
        }

        public static List<Dictionary<string, string>> GetLast10()
        {
            List<Dictionary<string, string>> glist = new List<Dictionary<string, string>>();
            try
            {
                BBSDataContext bbs = GetContext();
                var gl2 = bbs.CallLogs.OrderByDescending(p => p.Connected).Take(10)
                    .Join(bbs.Users, g => g.UserId, u => u.UserId, (g, u) => new { connected = g.Connected,  user = u.Username })
                    .ToList();

                foreach (var gl in gl2)
                {
                    Dictionary<string, string> d = new Dictionary<string, string>();
                    d.Add("when", gl.connected.ToString("yyyy-MM-dd hh:mm"));
                    d.Add("user", gl.user);
                    glist.Add(d);
                }
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetLast10: " + e.ToString());
            }
            return glist;
        }

        public static int RecordConnection(int userid)
        {
            int i = -1;
            BBSDataContext bbs = GetContext();
            CallLog cl = new CallLog() { Connected = DateTime.Now, Disconnected = DateTime.Now, UserId = userid };
            bbs.CallLogs.Add(cl);
            bbs.SaveChanges();
            return i;
        }

        public static void UpdateCallLog(int callLogId, int userid) //Used after new user reg
        {
            BBSDataContext bbs = GetContext();
            CallLog cl = bbs.CallLogs.FirstOrDefault(p => p.CallLogId.Equals(callLogId));
            if (cl != null)
            {
                cl.UserId = userid;
                bbs.SaveChanges();
            }
        }
        public static void RecordDisconnection(int callLogId)
        {
            BBSDataContext bbs = GetContext();
            CallLog cl = bbs.CallLogs.FirstOrDefault(p => p.CallLogId.Equals(callLogId));
            if (cl != null)
            {
                cl.Disconnected = DateTime.Now;
                bbs.SaveChanges();
            }
        }

        public static List<IdAndKeys> GetAllUserDefinedFieldsWithKey(string key)
        {
            BBSDataContext bbs = GetContext();
            List<IdAndKeys> idklist = new List<IdAndKeys>();
            List<UserDefinedField> udflist = bbs.UserDefinedFields.Where(p => p.Key.ToUpper().Equals(key.ToUpper())).ToList();
            if (udflist != null)
            {
                if (udflist.Count > 0)
                {
                    foreach (UserDefinedField udf in udflist)
                    {
                        IdAndKeys idk = new IdAndKeys();
                        idk.Id = udf.UserId;
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

        public static string GetUserDefinedField(int userid, string key)
        {
            BBSDataContext bbs = GetContext();
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

        public static void SaveUserDefinedField(int userid, string key, string value)
        {
            BBSDataContext bbs = GetContext();
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
                bbs.UserDefinedFields.Add(udf);
            }
            bbs.SaveChanges();

        }

        public static void AppendUserDefinedField(int userid, string key, string value)
        {
            String s = GetUserDefinedField(userid, key);
            s = s + value;
            SaveUserDefinedField(userid, key, s);
        }


    }
}
