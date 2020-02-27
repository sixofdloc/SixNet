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
                    if (gfa.ParentAreaId == null)
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


        public List<IdAndKeys> MessageBase_List_Area(int? area, int userid)
        {
            List<IdAndKeys> response = new List<IdAndKeys>();
            try
            {
                User u = _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userid));
                //TODO: Fix access groups
                //AccessGroup ag = _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<MessageBaseArea> arealist = _bbsDataContext.MessageBaseAreas.Where(p => p.ParentAreaId.Equals(area)).ToList();
                List<MessageBase> baselist = _bbsDataContext.MessageBases.Where(p => p.MessageBaseAreaId.Equals(area)).ToList();
                int listId = 1;
                foreach (MessageBaseArea gfa in arealist)
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
                foreach (MessageBase gfd in baselist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = gfd.Id
                    };
                    idak.Keys.Add("type", "file");
                    idak.Keys.Add("title", gfd.Title);
                    idak.Keys.Add("desc", gfd.Description);
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

                MessageBaseMessage reply = new MessageBaseMessage() { MessageBaseId = messagebase, Anonymous = anon, Posted = DateTime.Now, Subject = subject, UserId = userid, MessageThreadId = threadid, Body = message };
                _bbsDataContext.Create(reply);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }

        //Initialize and return a MessageThread
        public MessageThread InitMessageThread(int messageBaseId)
        {
            var messageThread = new MessageThread()
            {
                MessageBaseId = messageBaseId
            };
            return _bbsDataContext.Create(messageThread);
        }

        //Initialize and return a MessageHeader
        public MessageBaseMessage InitMessageBaseMessage(MessageThread messageThread, string subject, bool anonymous, int postingUserId, string messageBody)
        {
            var messageHeader = new MessageBaseMessage()
            {
                MessageBaseId = messageThread.MessageBaseId,
                MessageThreadId = messageThread.Id,
                Subject = subject,
                Anonymous = anonymous,
                UserId = postingUserId,
                Posted = DateTime.Now, 
                Body = messageBody
            };
            return _bbsDataContext.Create(messageHeader);
        }


        //Start new thread
        public void PostMessageAsNewThread(int messageBaseId, string subject, bool anonymous, int postingUserId, string messageBody)
        {
            try
            {
                var messageThread = InitMessageThread(messageBaseId);
                InitMessageBaseMessage(messageThread, subject, anonymous, postingUserId,messageBody);//Persists all changes
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.PostMessage: " + e.ToString());
            }
        }

        public List<ThreadListRow> ListThreadsForBase(int messageBase)
        {
            var result = new List<ThreadListRow>();
            try
            {
                var threads = _bbsDataContext.MessageThreads.Where(p => p.MessageBaseId == messageBase).OrderBy(p => p.Id).ToList();
                threads.ForEach(thread => {
                    var firstMessage = thread.MessageBaseMessages.OrderBy(p => p.Posted).First();
                    var lastMessage = thread.MessageBaseMessages.OrderBy(p => p.Posted).Last();
                    result.Add(
                        new ThreadListRow()
                        {
                            Subject = firstMessage.Subject,
                            LastActivity = lastMessage.Posted,
                            MessageThreadId = thread.Id,
                            Poster = lastMessage.User.Username,
                            PosterId = (int)lastMessage.UserId
                        }
                        );
                });
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.ListThreadsForBase(" + messageBase.ToString() + "): " + e.ToString());
            }
            return result;
        }


        public void MarkRead(int userId, int messageBaseMessageId)
        {
            try
            {
                var readRecord = new UserHasReadMessage()
                {
                    MessageBaseMessageId = messageBaseMessageId,
                    UserId = userId
                };
                _bbsDataContext.Create(readRecord);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MarkRead(" + userId.ToString() + "," + messageBaseMessageId.ToString() + "):" + e.ToString());
            }
        }

        public bool HasRead(int userid, int messageid)
        {
            return _bbsDataContext.UserHasReadMessages.Any(p => p.UserId == userid && p.MessageBaseMessageId == messageid);
        }

        public int? FirstUnread(int userId, List<int> messageIds)
        {
            int? result = null;
            var unreadMessages = messageIds;
            if (messageIds != null)
            {
                var messagesUserHasReadFromMessageIds = 
                    _bbsDataContext.UserHasReadMessages.Where(p => p.UserId == userId && messageIds.Contains(p.MessageBaseMessageId)).Select(q=>q.MessageBaseMessageId).ToList();
                if (messagesUserHasReadFromMessageIds.Any())
                {
                    messageIds.RemoveAll(p => messagesUserHasReadFromMessageIds.Contains(p));
                }
                    result = unreadMessages.FirstOrDefault();

            }
            return result;
        }

        public List<int> MessageIdsInThread(int threadId)
        {
            List<int> result = null;
            try
            {
                result = _bbsDataContext.MessageBaseMessages.Where(p => p.MessageThreadId == threadId).Select(p => p.Id).ToList();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.MessageIdsInThread(" + threadId.ToString() + "):" + e.ToString());
            }
            return result;

        }

        public MessageBaseMessage GetMessage(int messageBaseMessageId)
        {
            MessageBaseMessage result = null;
            try
            {
                result = _bbsDataContext.MessageBaseMessages.FirstOrDefault(p => p.Id == messageBaseMessageId);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in DataInterface.GetMessage(" + messageBaseMessageId.ToString() + "):" + e.ToString());
            }
            return result;
        }
    }
}
