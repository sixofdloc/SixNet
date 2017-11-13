using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class CallLog
    {
        public int CallLogId { get; set; }
        public int UserId { get; set; }
        public DateTime Connected { get; set; }
        public DateTime Disconnected { get; set; }
    }
}
