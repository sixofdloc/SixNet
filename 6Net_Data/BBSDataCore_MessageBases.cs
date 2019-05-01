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

        public bool CreateMessageBaseArea(MessageBaseArea messageBaseArea)
        {
            bool b = false;
            try
            {
                _bbsDataContext.MessageBaseAreas.Add(messageBaseArea);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", messageBaseArea, e);
            }
            return b;
        }

        public bool UpdateMessageBaseArea(MessageBaseArea messageBaseArea)
        {
            bool b = false;
            try
            {

                var oldMessageBaseArea = GetMessageBaseAreaById(messageBaseArea.Id);
                if (oldMessageBaseArea != null)
                {
                    oldMessageBaseArea.Description = messageBaseArea.Description;
                    oldMessageBaseArea.Title = messageBaseArea.Title;
                    oldMessageBaseArea.ParentAreaId = messageBaseArea.ParentAreaId;
                    //TODO: Access Level System
                    _bbsDataContext.SaveChanges();
                    b = true;
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", messageBaseArea, e);
            }
            return b;
        }
        public MessageBaseArea GetMessageBaseAreaById(int id)
        {
            try
            {
                return _bbsDataContext.MessageBaseAreas.FirstOrDefault(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + id + ")", e);
                return null;
            }
        }
        public MessageBase GetMessageBaseById(int id)
        {
            try
            {
                return _bbsDataContext.MessageBases.FirstOrDefault(p => p.Id.Equals(id));
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + id + ")", e);
                return null;
            }
        }

        public bool CreateMessageBase(MessageBase messageBase)
        {
            bool b = false;
            try
            {

                _bbsDataContext.MessageBases.Add(messageBase);
                _bbsDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", messageBase, e);
            }
            return b;
        }

        public bool UpdateMessageBase(MessageBase messageBase)
        {
            bool b = false;
            try
            {
                var oldMessageBase = GetMessageBaseAreaById(messageBase.Id);
                if (oldMessageBase != null)
                {
                    oldMessageBase.Description = messageBase.Description;
                    oldMessageBase.Title = messageBase.Title;
                    oldMessageBase.ParentAreaId = messageBase.MessageBaseAreaId;
                    //TODO: Access Level System
                   // oldMessageBase.AccessLevel = messageBase.AccessLevel;
                    _bbsDataContext.SaveChanges();
                    b = true;
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error("Params, Exception: ", messageBase, e);
            }
            return b;
        }

        public string MessageBaseAreaName(int id)
        {
            try
            {
                return GetMessageBaseAreaById(id).Title;
            }
            catch (Exception e)
            {
                LoggingAPI.Error("(" + id + ")", e);
                return "Invalid";
            }
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
                MessageBaseArea gfa = _bbsDataContext.MessageBaseAreas.FirstOrDefault(p => p.Id.Equals(area));
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
            //try
            //{
            //    User u = _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userid));
            //    AccessGroup ag = _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
            //    List<MessageBaseArea> arealist = _bbsDataContext.MessageBaseAreas.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
            //    List<MessageBase> baselist = _bbsDataContext.MessageBases.Where(p => p.ParentAreaId.Equals(area) && p.AccessLevel <= ag.AccessGroupNumber).ToList();
            //    int listId = 1;
            //    foreach (MessageBaseArea gfa in arealist)
            //    {
            //        IdAndKeys idak = new IdAndKeys()
            //        {
            //            Id = gfa.Id
            //        };
            //        idak.Keys.Add("title", gfa.Title);
            //        idak.Keys.Add("type", "area");
            //        idak.Keys.Add("desc", gfa.Description);
            //        idak.Keys.Add("listid", listId.ToString());
            //        listId++;
            //        response.Add(idak);
            //    }
            //    foreach (MessageBase gfd in baselist)
            //    {
            //        IdAndKeys idak = new IdAndKeys()
            //        {
            //            Id = gfd.Id
            //        };
            //        idak.Keys.Add("type", "file");
            //        idak.Keys.Add("title", gfd.Title);
            //        idak.Keys.Add("desc", gfd.Description);
            //        idak.Keys.Add("listid", listId.ToString());
            //        listId++;
            //        response.Add(idak);
            //    }
            //}
            //catch (Exception e)
            //{
            //    LoggingAPI.LogEntry("Exception in DataInterface.MessageBase_List_Area(" + area.ToString() + ") : " + e.Message);
            //}
            return response;
        }

        public void PostReply(int messagebase, string subject, bool anon, int userid, string message, int threadid)
        {
            try
            {

                MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId = threadid };
                _bbsDataContext.MessageHeaders.Add(header);
                _bbsDataContext.SaveChanges();

                MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.Id };
                _bbsDataContext.MessageBodies.Add(body);

                _bbsDataContext.SaveChanges();
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

                //MessageThread thread = new MessageThread() { MessageBaseId = messagebase, MessageHeaderId = -1 };
                //_bbsDataContext.MessageThreads.Add(thread);
                //_bbsDataContext.SaveChanges();

                //MessageHeader header = new MessageHeader() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId = thread.MessageThreadId };
                //_bbsDataContext.MessageHeaders.Add(header);
                //_bbsDataContext.SaveChanges();

                //thread.MessageHeaderId = header.Id;
                //_bbsDataContext.SaveChanges();

                //MessageBody body = new MessageBody() { Body = message, MessageHeaderId = header.Id };
                //_bbsDataContext.MessageBodies.Add(body);

                //_bbsDataContext.SaveChanges();
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
                string query = "SELECT MT.MessageThreadId AS MessageThreadId, "
                               + " IM.Subject AS Subject, "
                               + " CASE WHEN LM.Posted IS NULL THEN"
                               + "  CASE WHEN IM.Posted IS NULL THEN"
                               + "      '1970-01-01 00:00:00' "
                               + "  ELSE IM.Posted END "
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

               //TODO: Rewrite this to use the ORM.  Fuck SQL in classes
                //threads = <ThreadListRow>(query).ToList<ThreadListRow>();
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
                //if (!HasRead(userid, messageid))
                //{
                //    BBSDataDataContext bbs = GetDataContext();
                //    UserRead ur = new UserRead() { UserId = userid, MessageHeaderId = messageid };
                //    bbs.UserReads.InsertOnSubmit(ur);
                //    bbs.SubmitChanges();
                //}
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MarkRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            }
        }

        public bool HasRead(int userid, int messageid)
        {
            bool b = false;
            //try
            //{
            //    BBSDataDataContext bbs = GetDataContext();
            //    b = bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(messageid);
            //}
            //catch (Exception e)
            //{
            //    LoggingAPI.LogEntry("Exception in DataInterface.HasRead(" + userid.ToString() + "," + messageid.ToString() + "):" + e.ToString());
            //}
            return b;
        }

        public int FirstUnread(int userid, List<int> messageids)
        {
            int i = -1;
            //if (messageids != null)
            //{
            //    if (messageids.Count > 0)
            //    {
            //        try
            //        {
            //            BBSDataDataContext bbs = GetDataContext();
            //            i = messageids.FirstOrDefault(q => !(
            //                bbs.UserReads.Where(p => p.UserId.Equals(userid)).Select(r => r.MessageHeaderId).Contains(q))
            //                );
            //            //   if (i == null) i = -1;
            //        }
            //        catch (Exception e)
            //        {
            //            LoggingAPI.LogEntry("Exception in DataInterface.FirstUnread(" + userid.ToString() + ",LIST):" + e.ToString());
            //        }
            //    }
            //}
            return i;
        }

        public List<int> MessageIdsInThread(int threadid)
        {
            List<int> result = new List<int>();
            try
            {
                String Query = "SELECT MessageHeaderId FROM MessageHeaders WHERE MessageThreadId = " + threadid.ToString() + "ORDER BY Posted ";

            //    result = bbs.ExecuteQuery<int>(Query).ToList<int>();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageIdsInThread(" + threadid.ToString() + "):" + e.ToString());
            }
            return result;

        }

        //This is unneeded, you just use the headerid and the rest is lazy loaded
        public MessageHeader GetMessage(int MessageId)
        {
            return _bbsDataContext.MessageHeaders.FirstOrDefault(p => p.Id == MessageId);
        //    BBS_Message bm = new BBS_Message();
        //    bm.Header = bbs.MessageHeaders.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
        //    bm.Body = bbs.MessageBodies.FirstOrDefault(p => p.MessageHeaderId.Equals(MessageId));
        //    return bm;
        }
    }
}
