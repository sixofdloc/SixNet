using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data
{
    public class AccessToken
    {
        public int AccessTokenId { get; set; }
        public int UserId { get; set; }
        public int ResourceTypeId { get; set; }
        public int AccessTokenType { get; set; } // 0 for leveltoken, 1 for resource-specific token, 2 for resource-specific leveltoken
        public int ResourceId { get; set; } 
        public int AccessLevel { get; set; }
    }
}
