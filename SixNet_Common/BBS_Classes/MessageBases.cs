using System;
using System.Collections.Generic;
using System.Linq;
using SixNet_BBS.BBS_Classes.Editors;
using SixNet_BBS_Data;
using SixNet_StringUtils;


namespace SixNet_BBS.BBS_Classes
{
    class MessageBases
    {
        private readonly BBS _bbs;
        private readonly DataInterface _dataInterface;

        private int Current_Area = -1;
        //private int Current_Parent_Area = -1;
        private int CurrentMessageBase = -1;

        private List<IdAndKeys> Current_Area_List = null;
        private List<ThreadListRow> Current_Thread_List = null;

        private string Path;

        public MessageBases(BBS bbs,DataInterface dataInterface)
        {
            _bbs = bbs;
            _dataInterface = dataInterface;
            Current_Area = -1;
            //Current_Parent_Area = -1;
            _bbs.SendFileForTermType("messagebase_entry_root", true);
            CMD_List();
            RecalculatePath();
        }

        private void RecalculatePath()
        {
            string s = "";
            Path = "~c6/Main/";
            if (Current_Area > -1)
            {
                bool done = false;
                int tmparea = Current_Area;
                while (!done)
                {
                    IdAndKeys parentarea = _dataInterface.MessageBase_ParentArea(tmparea);
                    if (parentarea.Id > -1)
                    {
                        s = parentarea.Keys["title"] + "/" + s;
                    }
                    else
                    {
                        done = true;
                    }
                }
                s = s + _dataInterface.MessageBaseAreas().First(p => p.MessageBaseAreaId.Equals(Current_Area)).Title + "/";
            }
            else
            {
               // s = "Main";
            }
            Path = Path + s;
            if (CurrentMessageBase > -1) Path = Path + Current_Area_List.FirstOrDefault(p => p.Id.Equals(CurrentMessageBase)).Keys["title"] + ":";
            Path = Path + "~c1";

        }

        public void Prompt()
        {
            bool quitflag = false;
            while ((!quitflag) && _bbs.Connected)
            {
                //Show Main Prompt
                _bbs.WriteLine("~l1"+Path);
                if (!_bbs.ExpertMode)
                {
                    _bbs.WriteLine("~c7? ~c1Menu, ~c7H~c1elp~c2, ~c7L~c1ist~c2, ~c7Q~c1uit");
                }
                else
                {
                    _bbs.WriteLine();
                }
                if (CurrentMessageBase > -1)
                {
                    _bbs.Write("~c1Bases~c2:~c7");
                }
                else
                {
                    _bbs.Write("~c1Bases~c2:~c7");
                }
                string command = _bbs.Input(true, false, false);
                if (command.Length > 0)
                {
                    if ("0123456789".Contains(command.Substring(0,1)))
                    {
                        if (CurrentMessageBase == -1)
                        {
                            //Select item.
                            IdAndKeys selectedItem = Current_Area_List.FirstOrDefault(p => p.Keys["listid"].Equals(command));
                            if (selectedItem != null)
                            {
                                if (selectedItem.Keys["type"] == "area")
                                {
                                    _bbs.WriteLine("~l1~c7Changing to Area: " + selectedItem.Keys["title"]+"~p1");
                                    ChangeToArea(selectedItem.Id);
                                }
                                else
                                {
                                    _bbs.WriteLine("~l1~c7Changing to Message Base: " + selectedItem.Keys["title"] + "~p1");
                                    ChangeToMessageBase(selectedItem.Id);
                                }
                            }
                        }
                        else
                        {
                            //We're in a messagebase \
                            CMD_ReadThreadByListId(int.Parse(command));
                        }
                    }
                    else
                    {

                        switch (command.ToUpper()[0])
                        {
                            case 'H':
                                if (CurrentMessageBase == -1)
                                {
                                    _bbs.SendFileForTermType("messagearea_help", true);
                                }
                                else
                                {
                                    _bbs.SendFileForTermType("messagebase_help", true);
                                }
                                break;
                            case '?':
                                if (CurrentMessageBase == -1)
                                {
                                    _bbs.SendFileForTermType("messagearea_menu", true);
                                }
                                else
                                {
                                    _bbs.SendFileForTermType("messagebase_menu", true);
                                }
                                break;
                            case 'L':
                                CMD_List();
                                break;
                            case '/':
                                if (CurrentMessageBase > -1)
                                {
                                    CurrentMessageBase = -1;
                                    RecalculatePath();
                                    CMD_List();
                                }
                                else
                                {
                                    if (Current_Area > -1)
                                    {
                                        CurrentMessageBase = -1;
                                        IdAndKeys mba = _dataInterface.MessageBase_ParentArea(Current_Area);
                                        _bbs.WriteLine("~l1~c7Changing to Area: " + mba.Keys["title"] + "~p1");
                                        ChangeToArea(mba.Id);
                                    }
                                    else
                                    {
                                        _bbs.WriteLine("~l1~d2Already at top level.~d0");
                                    }
                                }
                                break;
                            case 'P':
                                if (CurrentMessageBase == -1)
                                {
                                    _bbs.WriteLine("~l1~d1Select a message base.~d0");
                                }
                                else
                                {
                                    CMD_Post();
                                }
                                break;
                            case 'Q':
                                quitflag = true;
                                break;
                            case 'R':
                                // "Unread" means the message id does not appear in the 
                                // R = Read next unread message
                                // RA = Read all messages in all threads
                                // RA# = Read all messages in specified thread
                                // R# = Read unread messages in specified thread
                                // R#,# = Read specified message in specified thread
                                // RN = Read unread messages in all threads with unread messages
                                if (CurrentMessageBase == -1)
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
                                break;
                        }
                    }
                }
            }
        }


        public void ChangeToArea(int areaId)
        {
            //Select Area
            Current_Area = areaId;
            if (areaId < 0)
            {
                _bbs.SendFileForTermType("messagebase_entry_root", true);
            }
            else
            {
                _bbs.SendFileForTermType("messagebase_area_" + Current_Area.ToString(), true);
            }
            Current_Area_List = _dataInterface.MessageBase_List_Area(areaId, _bbs.CurrentUser.UserId);
            CurrentMessageBase = -1;
            RecalculatePath();

        }

        public void ChangeToMessageBase(int baseid)
        {
            //Select Area
            CurrentMessageBase = baseid;
            _bbs.SendFileForTermType("messagebase_entry_" + Current_Area.ToString(), true);
            //Show stats about this base
            Current_Thread_List = _dataInterface.ListThreadsForBase(baseid);
            RecalculatePath();

        }

        public void CMD_Post()
        {
            _bbs.Write("~l1~c1Post Message~l1Anonymous?");
            bool anon = _bbs.YesNo(true, true);
            _bbs.Write("~c1~l1Subject ~c7:~c1");
            string subject = _bbs.Input(true, false, false);
            if (subject != "")
            {
                Line_Editor le = new Line_Editor(_bbs);
                if (le.Edit(null))
                {
                    _bbs.Write("~s1~l1~c1Posting Message...");
                    _dataInterface.PostMessage(CurrentMessageBase, subject, anon, _bbs.CurrentUser.UserId, le.GetMessage());
                    _bbs.WriteLine("Done.");
                }
            }
        }

        public void CMD_List()
        {
            if (CurrentMessageBase == -1)
            {
                _bbs.WriteLine("");
                _bbs.WriteLine("");
                Current_Area_List = _dataInterface.MessageBase_List_Area(Current_Area, _bbs.CurrentUser.UserId);
                foreach (IdAndKeys idak in Current_Area_List)
                {
                    if (idak.Keys["type"] == "area")
                    {
                        _bbs.WriteLine("~c7" + idak.Keys["listid"] + "~c1. ~cf" + idak.Keys["title"]+" (area)");
                    }
                    else
                    {
                        _bbs.WriteLine("~c7"+idak.Keys["listid"] + "~c1. " + idak.Keys["title"]);
                    }
                }
                _bbs.WriteLine("");
            }
            else
            {
                //List messages
                _bbs.Write("~s1~d2" + Utils.Center("THREADS IN CURRENT BASE", _bbs.TerminalType.Columns()) + "~d0");
                //Pull a new list each time
                Current_Thread_List = _dataInterface.ListThreadsForBase(CurrentMessageBase);
                if (Current_Thread_List.Count > 0)
                {
                    foreach (ThreadListRow tlr in Current_Thread_List){
                        if (tlr.PosterId == -1)
                        {
                            //skip this row
                        }
                        else
                        {
                            if (_bbs.TerminalType.Columns() == 40)
                            {
                                //                      1111111111222222222233333333334444444444555555555566666666666777777777
                                //Columns are 01234567890123456789012345678901234567890123456789012345678901234567890123456789
                                //             ID- SUBJECT---------------------------
                                //                 POSTED-----  POSTER--------------- 
                                _bbs.Write("~c7" + Utils.Clip(Current_Thread_List.IndexOf(tlr).ToString(), 4, true) + "~c1");
                                _bbs.WriteLine(" " + Utils.Clip(tlr.Subject, 32, true));

                                _bbs.Write(Utils.Clip("~c1Last Post:~c3" + tlr.LastActivity.ToString("yy-MM-dd hh:mm"), 30, true));
                                _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Poster, 10, true));

                            }
                            else
                            {
                                //             ID- SUBJECT-------------------------------------- POSTED----- POSTER---------      
                                _bbs.Write("~c7" + Utils.Clip(Current_Thread_List.IndexOf(tlr).ToString(), 4, true) + "~c1");
                                _bbs.Write(" " + Utils.Clip(tlr.Subject, 40, true));
                                _bbs.Write("~c3 " + Utils.Clip(tlr.LastActivity.ToString("yy-MM-dd hh:mm"), 14, true));
                                _bbs.WriteLine("~c4 " + Utils.Clip(tlr.Poster, 21, true));
                            }
                        }
                    }
                }

                else
                {
                    _bbs.WriteLine("~c7Nothing Found...~c1");
                }
                _bbs.WriteLine("~d2" + Utils.SPC(_bbs.TerminalType.Columns()) + "~d0");
            }
        }

        public void CMD_ReadNextUnread()
        {
            //Read Next unread message
            //Search through each thread until an unread message is found.
            int i = -1;
            foreach (ThreadListRow tlr in Current_Thread_List)
            {
                List<int> messages = _dataInterface.MessageIdsInThread(tlr.MessageThreadId);
                 i = _dataInterface.FirstUnread(_bbs.CurrentUser.UserId, messages);
                if (i != -1)
                {
                    break;
                }
               
            }
            if (i != -1) CMD_ReadMessage(i,true);

        }

        public void CMD_ReadThreadByListId(int listid)
        {
            if ((listid > Current_Thread_List.Count - 1) || (listid < 0))
            {
                _bbs.WriteLine("~l2~d2NO SUCH THREAD.~g1~d0");
            }
            else
            {
                ThreadListRow selectedThread = Current_Thread_List[listid];
                if (selectedThread != null)
                {
                    CMD_ReadThread(selectedThread.MessageThreadId, -1);
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
        public void CMD_ReadThread(int ThreadId, int StartWithMessageId)
        {
            List<int> messagesinthread = _dataInterface.MessageIdsInThread(ThreadId);
            if (StartWithMessageId == -1)
            {
                int start = 0;
                    for (int x = start; x<messagesinthread.Count; x++)
                    {
                        CMD_ReadMessage(messagesinthread[x],(x==start));
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
                if (messagesinthread.Contains(StartWithMessageId))
                {
                    int start = messagesinthread.IndexOf(StartWithMessageId);
                    for (int x = start; x < messagesinthread.Count; x++)
                    {
                        CMD_ReadMessage(messagesinthread[x],(x==start));
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

        public void CMD_ReadMessage(int MessageId, bool showsubject)
        {
            BBS_Message bm = _dataInterface.GetMessage(MessageId);
            if (bm == null)
            {
                _bbs.WriteLine("~l1~d2NO SUCH MESSAGE.~d0");
            }
            else
            {
                _bbs.WriteLine("~l1");
                _bbs.WriteLine("~c1Poster: ~c7" + bm.Header.User.Username );
                if (showsubject) _bbs.WriteLine("~c1Subject: ~c7" + bm.Header.Subject);
                _bbs.WriteLine("~c1Posted: ~c7" + bm.Header.Posted.ToString("yyyy-MM-dd hh:mm:ss"));

                _bbs.Write("~c2" + Utils.Repeat('\xc0', _bbs.TerminalType.Columns()));
                _bbs.Write("~c1");
                string[] splitarray = { "~\xff~" };
                string[] lines = bm.Body.Body.TrimEnd("~\xff".ToCharArray()).Split(splitarray, StringSplitOptions.None);
                foreach (string s in lines)
                {
                    _bbs.WriteLine(s);
                }
                _bbs.Write("~c2");
                _bbs.Write(Utils.Repeat('\xc0', _bbs.TerminalType.Columns()));
            }
        }

        public void CMD_Reply(int ThreadId)
        {
            _bbs.Write("~l1~c1Reply To Message~l1Anonymous?");
            bool anon = _bbs.YesNo(true, true);

            string subject = "RE:" + Current_Thread_List.First(p => p.MessageThreadId.Equals(ThreadId)).Subject;
            if (subject != "")
            {
                Line_Editor le = new Line_Editor(_bbs);
                if (le.Edit(null))
                {
                    _bbs.Write("~s1~l1~c1Posting Message...");
                    _dataInterface.PostReply(CurrentMessageBase, subject, anon, _bbs.CurrentUser.UserId, le.GetMessage(), ThreadId);
                    _bbs.WriteLine("Done.");
                }
            }

        }

    }
}
