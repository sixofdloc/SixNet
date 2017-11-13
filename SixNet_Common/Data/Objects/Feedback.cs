using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int FromUser { get; set; }
        public DateTime Sent { get; set; }
    }
}
