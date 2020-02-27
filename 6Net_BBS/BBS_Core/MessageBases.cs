using System;
using System.Collections.Generic;
using System.Linq;
using Net_BBS.BBS_Core.Editors;
using Net_Data;
using Net_Data.Classes;
using Net_Data.Models;
using Net_StringUtils;

namespace Net_BBS.BBS_Core
{
    class MessageBases
    {
        private readonly BBS _bbs;
        private readonly BBSDataCore _bbsDataCore;

        private int? currentArea;
        private int? currentMessageBase;

        private List<IdAndKeys> currentAreaList;
        private List<ThreadListRow> currentThreadList;

        private string currentAreaAndBasePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Net_BBS.BBS_Core.MessageBases"/> class.
        /// </summary>
        /// <param name="bbs">Bbs.</param>
        /// <param name="bbsDataCore">Bbs data core.</param>
        public MessageBases(BBS bbs, BBSDataCore bbsDataCore)
        {
            _bbs = bbs;
            _bbsDataCore = bbsDataCore;
            currentArea = null;
            _bbs.SendFileForTermType("messagebase_entry_root", true);
            CMD_List();
            RecalculatePath();
        }

        /// <summary>
        /// Recalculates the path.
        /// </summary>
        private void RecalculatePath()
        {
            var s = "";
            currentAreaAndBasePath = "~cd/Main/";
            if (currentArea != null)
            {
                var done = false;
                var tmparea = currentArea;
                while (!done)
                {
                    var parentarea = _bbsDataCore.MessageBase_ParentArea((int)tmparea);
                    if (parentarea.Id != null)
                    {
                        s = parentarea.Keys["title"] + "/" + s;
                    }
                    else
                    {
                        done = true;
                    }
                }
                s = s + _bbsDataCore.MessageBaseAreas().First(p => p.ParentAreaId.Equals(currentArea)).Title + "/";
            }
            currentAreaAndBasePath = currentAreaAndBasePath + s;
            if (currentMessageBase != null) currentAreaAndBasePath = currentAreaAndBasePath + currentAreaList.FirstOrDefault(p => p.Id.Equals(currentMessageBase)).Keys["title"] + ":";
            currentAreaAndBasePath = currentAreaAndBasePath + "~c1";

        }

        /// <summary>
        /// Main loop for message base system
        /// </summary>
        public void Prompt()
        {
            var quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
                _bbs.WriteLine("~l2" + currentAreaAndBasePath);
                _bbs.WriteLine(_bbs.expertMode ? "" : "~c7? ~c1Menu, ~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                _bbs.Write("~c1Bases~c2:~c7");
                var command = _bbs.Input(true, false, false);
                if (command.Length > 0)
                {
                    if ("0123456789".Contains(command.Substring(0, 1)))
                    {
                        CMD_Num(command);
                    }
                    else
                    {

                        switch (command.ToUpper()[0])
                        {
                            case 'H':
                                _bbs.SendFileForTermType((currentMessageBase==null)?"messagearea_help" : "messagebase_help", true);
                                break;
                            case '?':
                                _bbs.SendFileForTermType((currentMessageBase == null) ? "messagearea_menu" : "messagebase_menu", true);
                                break;
                            case 'L':
                                CMD_List();
                                break;
                            case '/':
                                CMD_FolderUp();
                                break;
                            case 'P':
                                CMD_Post();
                                break;
                            case 'Q':
                                quitflag = true;
                                break;
                            case 'R':
                                CMD_Read(command);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Read a message, top level command handler.  
        /// Decides which sub-command to use here.
        /// </summary>
        /// <param name="command">Full text of the command entered by the user</param>
        public void CMD_Read(string command)
        {
            // "Unread" means the message id does not appear in an entry in UserHasReadMessages for this userId 
            // R = Read next unread message
            // RA or R A or R All = Read all messages in all threads
            // RA# = Read all messages in specified thread
            // R# = Read unread messages in specified thread
            // R#,# = Read specified message in specified thread
            // RN = Read unread messages in all threads with unread messages
            if (currentMessageBase == null)
            {
                _bbs.WriteLine("~l1~d1Select a message base.~d0");
            }
            else
            {
                if (command.Length > 1)
                {
                    if (command.ToUpper()[1] == 'A')
                    {
                        //RA, RA#

                    }
                    else
                    {
                        if (command.ToUpper()[1] == 'N')
                        {
                            //RN,RN#
                            //CMD_ReadNewMessages();
                            CMD_ReadNextUnread();
                        }
                        else
                        {
                            if ("1234567890".Contains(command.ToUpper()[1]))
                            {
                                //R#

                            }
                        }
                    }
                }
                else
                {
                    CMD_ReadNextUnread();
                }
            }

        }

        public void CMD_Num(string command)
        {
            if (currentMessageBase == null) //We're not in a message base
            {
                //Select area or message base to change to
                var selectedItem = currentAreaList.FirstOrDefault(p => p.Keys["listid"].Equals(command));
                if (selectedItem != null)
                {
                    if (selectedItem.Keys["type"] == "area")
                    {
                        _bbs.WriteLine("~l1~c7Changing to Area: " + selectedItem.Keys["title"]);
                        ChangeToArea(selectedItem?.Id);
                    }
                    else
                    {
                        _bbs.WriteLine("~l1~c7Changing to Message Base: " + selectedItem.Keys["title"]);
                        ChangeToMessageBase((int)selectedItem.Id);
                    }
                }
                else
                {
                    _bbs.WriteLine("~l1~d1No such message area or base  was found.~d0");
                }
            }
            else
            {
                //We're in a messagebase \
                CMD_ReadThreadByListId(int.Parse(command));
            }
        }

        public void CMD_FolderUp()
        {
            if (currentMessageBase != null)
            {
                currentMessageBase = null;
                RecalculatePath();
                CMD_List();
            }
            else
            {
                if (currentArea != null)
                {
                    currentMessageBase = null;
                    var mba = _bbsDataCore.MessageBase_ParentArea((int)currentArea);
                    _bbs.WriteLine("~l1~c7Changing to Area: " + mba.Keys["title"] + "~p1");
                    ChangeToArea(mba?.Id);
                }
                else
                {
                    _bbs.WriteLine("~l1~d2Already at top level.~d0");
                }
            }
        }

        public void ChangeToArea(int? areaId)
        {
            //Select Area, null is the root area
            currentArea = areaId;
            _bbs.SendFileForTermType((areaId==null)?"messagebase_entry_root" : "messagebase_area_" + currentArea?.ToString(), true);
            currentAreaList = _bbsDataCore.MessageBase_List_Area(areaId, _bbs.currentUser.Id);
            currentMessageBase = null;
            RecalculatePath();

        }

        public void ChangeToMessageBase(int baseid)
        {
            //Select Area
            currentMessageBase = baseid;
            _bbs.SendFileForTermType("messagebase_entry_" + currentArea.ToString(), true);
            //Show stats about this base
            currentThreadList = _bbsDataCore.ListThreadsForBase(baseid);
            RecalculatePath();

        }

        public void CMD_Post()
        {
            if (currentMessageBase == null)
            {
                _bbs.WriteLine("~l1~d1Select a message base.~d0");
            }
            else
            {

                _bbs.Write("~l1~c1Post Message~l1Anonymous?");
                var anon = _bbs.YesNo(true, true);
                _bbs.Write("~c1~l1Subject ~c7:~c1");
                var subject = _bbs.Input(true, false, false);
                if (subject != "")
                {
                    Line_Editor le = new Line_Editor(_bbs);
                    if (le.Edit(null))
                    {
                        _bbs.Write("~s1~l1~c1Posting Message...");
                        _bbsDataCore.PostMessageAsNewThread((int)currentMessageBase, subject, anon, _bbs.currentUser.Id, le.GetMessage());
                        _bbs.WriteLine("Done.");
                    }
                }
            }
        }

        /// <summary>
        /// List all messages in the currently selected message base OR
        /// List all message bases in the currently selected message base area
        /// </summary>
        public void CMD_List()
        {
            if (currentMessageBase == null)
            {
                //List bases/areas in current area
                _bbs.WriteLine("");
                _bbs.WriteLine("");
                currentAreaList = _bbsDataCore.MessageBase_List_Area(currentArea, _bbs.currentUser.Id);
                foreach (IdAndKeys idak in currentAreaList)
                {
                    if (idak.Keys["type"] == "area")
                    {
                        _bbs.WriteLine("~c7" + idak.Keys["listid"] + "~c1. ~cf" + idak.Keys["title"] + " (area)");
                    }
                    else
                    {
                        _bbs.WriteLine("~c7" + idak.Keys["listid"] + "~c1. " + idak.Keys["title"]);
                    }
                }
                _bbs.WriteLine("");
            }
            else
            {
                //List messages
                _bbs.Write("~s1~d2" + Utils.Center("THREADS IN CURRENT BASE", _bbs.terminalType.Columns()) + "~d0");
                //Pull a new list each time
                currentThreadList = _bbsDataCore.ListThreadsForBase((int)currentMessageBase);
                if (currentThreadList.Count > 0)
                {
                    foreach (ThreadListRow tlr in currentThreadList)
                    {
                        if (_bbs.terminalType.Columns() == 40)
                        {
                            //                      1111111111222222222233333333334444444444555555555566666666666777777777
                            //Columns are 01234567890123456789012345678901234567890123456789012345678901234567890123456789
                            //             ID- SUBJECT---------------------------
                            //                 POSTED-----  POSTER--------------- 
                            _bbs.Write("~c7" + Utils.Clip(currentThreadList.IndexOf(tlr).ToString(), 4, true) + "~c1");
                            _bbs.WriteLine(" " + Utils.Clip(tlr.Subject, 32, true));

                            _bbs.Write(Utils.Clip("~c1Last Post:~c3" + tlr.LastActivity.ToString("yy-MM-dd hh:mm"), 30, true));
                            _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Poster, 10, true));

                        }
                        else
                        {
                            //             ID- SUBJECT-------------------------------------- POSTED----- POSTER---------      
                            _bbs.Write("~c7" + Utils.Clip(currentThreadList.IndexOf(tlr).ToString(), 4, true) + "~c1");
                            _bbs.Write(" " + Utils.Clip(tlr.Subject, 40, true));
                            _bbs.Write("~c3 " + Utils.Clip(tlr.LastActivity.ToString("yy-MM-dd hh:mm"), 14, true));
                            _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Poster, 21, true));
                        }
                    }
                }

                else
                {
                    _bbs.WriteLine("~c7Nothing Found...~c1");
                }
                _bbs.WriteLine("~d2" + Utils.SPC(_bbs.terminalType.Columns()) + "~d0");
            }
        }


        public void CMD_ReadNextUnread()
        {
            //Read Next unread message
            //Search through each thread until an unread message is found.
            int? i = null;
            foreach (var tlr in currentThreadList)
            {
                var messages = _bbsDataCore.MessageIdsInThread(tlr.MessageThreadId);
                if (messages != null)
                {
                    i = _bbsDataCore.FirstUnread(_bbs.currentUser.Id, messages);
                    if (i != null)
                    {
                        break;
                    }
                }
            }
            if (i != null) CMD_ReadMessage((int)i, true);

        }

        public void CMD_ReadThreadByListId(int listid)
        {
            if ((listid > currentThreadList.Count - 1) || (listid < 0))
            {
                _bbs.WriteLine("~l2~d2NO SUCH THREAD.~g1~d0");
            }
            else
            {
                ThreadListRow selectedThread = currentThreadList[listid];
                if (selectedThread != null)
                {
                    CMD_ReadThread(selectedThread.MessageThreadId, null);
                }
            }
        }

        public const int AMP_REPLY = 0;
        public const int AMP_QUIT = 1;
        public const int AMP_NEXT = 2;
        public const int AMP_READ_TO_END = 3;

        public int AfterMessagePrompt(bool next)
        {
            int tries = 0;
            int result = AMP_QUIT;
            while (tries < 3)
            {
                _bbs.Write("~c7R~c1eply,");
                if (next) _bbs.Write("~c7N~c1ext,readto~c7E~c1nd,");
                _bbs.Write("~c7Q~c1uit:~c7");
                char c = _bbs.GetChar();
                switch (c.ToString().ToUpper())
                {
                    case "R":
                        _bbs.WriteLine("Reply");
                        result = AMP_REPLY;
                        tries = 3;
                        break;
                    case "N":
                        if (next)
                        {
                            _bbs.WriteLine("Next");
                            result = AMP_NEXT;
                            tries = 3;
                        }
                        else
                        {
                            _bbs.Write("~g1");
                            tries++;
                            if (tries == 3)
                            {
                                _bbs.WriteLine("Quit");
                            }
                        }
                        break;
                    case "Q":
                        tries = 3;
                        _bbs.WriteLine("Quit");
                        break;
                    case "E":
                        tries = 3;
                        _bbs.WriteLine("ReadToEnd");
                        break;
                    default:
                        _bbs.Write("~g1");
                        tries++;
                        if (tries == 3)
                        {
                            _bbs.WriteLine("Quit");
                        }
                        break;
                }
            }
            _bbs.Write("~c1");
            return result;
        }

        //Read entire thread, or thread from message id, regardless of new
        public void CMD_ReadThread(int ThreadId, int? StartWithMessageId)
        {
            List<int> messagesinthread = _bbsDataCore.MessageIdsInThread(ThreadId);
            if (StartWithMessageId == null)
            {
                int start = 0;
                for (int x = start; x < messagesinthread.Count; x++)
                {
                    CMD_ReadMessage(messagesinthread[x], (x == start));
                    if (x < messagesinthread.Count - 1)
                    {
                        //Reply, Next, Quit
                        int amp = AfterMessagePrompt(true);
                        switch (amp)
                        {
                            case AMP_REPLY:
                                break;
                            case AMP_NEXT:
                                break;
                            case AMP_QUIT:
                                break;
                        }
                    }
                    else
                    {
                        //REply,Quit
                        int amp = AfterMessagePrompt(false);
                        if (amp == AMP_REPLY)
                        {
                            CMD_Reply(ThreadId);

                        }
                        if (amp == AMP_QUIT) break;
                    }

                }

            }
            else
            {
                //start with specific message id
                if (messagesinthread.Contains((int)StartWithMessageId))
                {
                    int start = messagesinthread.IndexOf((int)StartWithMessageId);
                    for (int x = start; x < messagesinthread.Count; x++)
                    {
                        CMD_ReadMessage(messagesinthread[x], (x == start));
                        if (x < messagesinthread.Count - 1)
                        {
                            //Reply, Next, Quit
                            int amp = AfterMessagePrompt(true);
                            switch (amp)
                            {
                                case AMP_REPLY:
                                    break;
                                case AMP_QUIT:
                                    break;
                            }
                        }
                        else
                        {
                            //REply,Quit
                            int amp = AfterMessagePrompt(false);
                        }

                    }

                }
            }
        }

        public void CMD_ReadMessage(int messageId, bool showSubject)
        {
            MessageBaseMessage messageBaseMessage = _bbsDataCore.GetMessage(messageId);
            if (messageBaseMessage == null)
            {
                _bbs.WriteLine("~l1~d2NO SUCH MESSAGE.~d0");
            }
            else
            {
                _bbs.WriteLine("~l1");
                _bbs.WriteLine("~c1Poster: ~c7" + (messageBaseMessage.Anonymous ? "Anonymous" : messageBaseMessage.User.Username));
                if (showSubject) _bbs.WriteLine("~c1Subject: ~c7" + messageBaseMessage.Subject);
                _bbs.WriteLine("~c1Posted: ~c7" + messageBaseMessage.Posted.ToString("yyyy-MM-dd hh:mm:ss"));

                _bbs.Write("~c2" + Utils.Repeat(_bbs.terminalType.Horizontal_Bar(), _bbs.terminalType.Columns()));
                _bbs.Write("~c1");
                string[] splitarray = { "~\xff~" };
                string[] lines = messageBaseMessage.Body.TrimEnd("~\xff".ToCharArray()).Split(splitarray, StringSplitOptions.None);
                foreach (string s in lines)
                {
                    _bbs.WriteLine(s);
                }

                _bbs.Write("~c2" + Utils.Repeat(_bbs.terminalType.Horizontal_Bar(), _bbs.terminalType.Columns()));
                _bbsDataCore.MarkRead(_bbs.currentUser.Id, messageId);
            }

        }

        public void CMD_Reply(int ThreadId)
        {
            _bbs.Write("~l1~c1Reply To Message~l1Anonymous?");
            bool anon = _bbs.YesNo(true, true);

            string subject = "RE:" + currentThreadList.First(p => p.MessageThreadId.Equals(ThreadId)).Subject;
            if (subject != "")
            {
                Line_Editor le = new Line_Editor(_bbs);
                if (le.Edit(null))
                {
                    _bbs.Write("~s1~l1~c1Posting Message...");
                    _bbsDataCore.PostReply((int)currentMessageBase, subject, anon, _bbs.currentUser.Id, le.GetMessage(), ThreadId);
                    _bbs.WriteLine("Done.");
                }
            }

        }

    }
}
