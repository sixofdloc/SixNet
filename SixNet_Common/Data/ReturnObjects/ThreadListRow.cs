using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.ReturnObjects
{
    public class ThreadListRow
    {
        public int MessageThreadId {get; set;}
        public string Subject { get; set; }
        public DateTime LastActivity { get; set; }
        public string Poster { get; set; }
        public int PosterId { get; set; }
        public int Replies { get; set; }
    }
}
