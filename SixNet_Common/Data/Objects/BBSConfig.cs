using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data
{
    public class BBSConfig
    {
        public int BBSConfigId { get; set; }
        public int SysOpUserId { get; set; }
        public string BBS_Name { get; set; }
        public string BBS_URL { get; set; }
        public int BBS_Port { get; set; }
        public string SysOp_Handle { get; set; }
        public string SysOp_Email { get; set; }

    }
}
