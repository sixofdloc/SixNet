using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class Graffiti
    {
        public int GraffitiId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime Posted { get; set; }
    }
}
