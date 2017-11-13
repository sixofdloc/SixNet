using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class News_Item
    {
        public int News_ItemId { get; set; }
        public DateTime Posted { get; set; }
        public int UserId { get; set; } //Userid of poster
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
