using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class UserMessageBase
    {
        //Key
        public int UserMessageBaseId { get; set; }
        
        //Foreign key to User
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //Foreign key to MessageBase
        public int MessageBaseId { get; set; }
        public virtual MessageBase MessageBase { get; set; }

        public int HighestMessageRead { get; set; }
    }
}
