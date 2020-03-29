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

        //TODO: make this just return the messagebasearea object instead of this complicated idak
        public IdAndKeys MessageBase_ParentArea(int areaId)
        {
            IdAndKeys newAreaIdAndFields = new IdAndKeys()
            {
                Id = -1
            };
            newAreaIdAndFields.Keys.Add("title", "Main");
            try
            {
                MessageBaseArea messageBaseArea = _bbsDataContext.MessageBaseAreas.FirstOrDefault(p => p.Id.Equals(areaId));
                if (messageBaseArea != null)
                {
                    newAreaIdAndFields.Id = messageBaseArea.ParentAreaId;
                    if (messageBaseArea.ParentAreaId == null)
                    {
                        newAreaIdAndFields.Keys["title"] = "Main";
                    }
                    else
                    {
                        newAreaIdAndFields.Keys["title"] = messageBaseArea.Title;
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId, newAreaIdAndFields }); 
            }
            return newAreaIdAndFields;
        }


        public List<IdAndKeys> MessageBase_List_Area(int? areaId, int userId)
        {
            List<IdAndKeys> listForArea = new List<IdAndKeys>();
            try
            {
                User user = _bbsDataContext.Users.FirstOrDefault(p => p.Id.Equals(userId));
                //TODO: Fix access groups
                //AccessGroup ag = _bbsDataContext.AccessGroups.FirstOrDefault(p => p.AccessGroupNumber.Equals(u.AccessLevel));
                List<MessageBaseArea> arealist = _bbsDataContext.MessageBaseAreas.Where(p => p.ParentAreaId.Equals(areaId)).ToList();
                List<MessageBase> baselist = _bbsDataContext.MessageBases.Where(p => p.MessageBaseAreaId.Equals(areaId)).ToList();
                int listId = 1;
                foreach (MessageBaseArea messageBaseArea in arealist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = messageBaseArea.Id
                    };
                    idak.Keys.Add("title", messageBaseArea.Title);
                    idak.Keys.Add("type", "area");
                    idak.Keys.Add("desc", messageBaseArea.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    listForArea.Add(idak);
                }
                foreach (MessageBase messageBase in baselist)
                {
                    IdAndKeys idak = new IdAndKeys()
                    {
                        Id = messageBase.Id
                    };
                    idak.Keys.Add("type", "file");
                    idak.Keys.Add("title", messageBase.Title);
                    idak.Keys.Add("desc", messageBase.Description);
                    idak.Keys.Add("listid", listId.ToString());
                    listId++;
                    listForArea.Add(idak);
                }
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { areaId, userId, listForArea }); 
            }
            return listForArea;
        }

        public void PostReply(int messageBaseId, string subject, bool PostAnonymous, int userId, string message, int threadId)
        {
            try
            {

                MessageBaseMessage reply = new MessageBaseMessage() { MessageBaseId = messageBaseId, Anonymous = PostAnonymous, Posted = DateTime.Now, Subject = subject, UserId = userId, MessageThreadId = threadId, Body = message };
                _bbsDataContext.Create(reply);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { messageBaseId, subject, PostAnonymous, userId, message, threadId }); 
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
        public void PostMessageAsNewThread(int messageBaseId, string subject, bool postAnonymous, int postingUserId, string messageBody)
        {
            try
            {
                var messageThread = InitMessageThread(messageBaseId);
                InitMessageBaseMessage(messageThread, subject, postAnonymous, postingUserId,messageBody);//Persists all changes
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { messageBaseId, subject, postAnonymous, postingUserId, messageBody }); 
            }
        }

        public List<ThreadListRow> ListThreadsForBase(int messageBaseId)
        {
            var threadListRows = new List<ThreadListRow>();
            try
            {
                var threads = _bbsDataContext.MessageThreads.Where(p => p.MessageBaseId == messageBaseId).OrderBy(p => p.Id).ToList();
                threads.ForEach(thread => {
                    var firstMessage = thread.MessageBaseMessages.OrderBy(p => p.Posted).First();
                    var lastMessage = thread.MessageBaseMessages.OrderBy(p => p.Posted).Last();
                    threadListRows.Add(
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
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { messageBaseId, threadListRows }); 
            }
            return threadListRows;
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
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { userId, messageBaseMessageId }); 
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
            List<int> messageIdList = null;
            try
            {
                messageIdList = _bbsDataContext.MessageBaseMessages.Where(p => p.MessageThreadId == threadId).Select(p => p.Id).ToList();
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { threadId,messageIdList });
            }
            return messageIdList;

        }

        public MessageBaseMessage GetMessage(int messageBaseMessageId)
        {
            MessageBaseMessage message = null;
            try
            {
                message = _bbsDataContext.MessageBaseMessages.FirstOrDefault(p => p.Id == messageBaseMessageId);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { messageBaseMessageId, message });
            }
            return message;
        }
    }
}
