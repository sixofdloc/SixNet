using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class UserRead
    {
        public int UserReadId { get; set; }
        public int UserId { get; set; }
        public int MessageHeaderId { get; set; }
    }
}
