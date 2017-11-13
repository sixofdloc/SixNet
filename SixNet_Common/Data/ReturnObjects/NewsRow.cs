using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.ReturnObjects
{
    public class NewsRow
    {
        public string Subject { get; set; }
        public DateTime Posted { get; set; }
        public string Body { get; set; }
    }
}
