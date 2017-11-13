using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class AccessGroup
    {
        public int AccessGroupId { get; set; }
        public int AccessGroupNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CallsPerDay { get; set; }
        public int MinutesPerCall { get; set; }
        public bool Flag_Remote_Maintenance { get; set; }
        public bool Is_SysOp { get; set; }
    }
}
