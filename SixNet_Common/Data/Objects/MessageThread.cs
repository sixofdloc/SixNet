using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class MessageThread
    {
        public int MessageThreadId { get; set; }
        public int MessageBaseId { get; set; }
        public int InitialMessageHeaderId { get; set; }
    }
}
