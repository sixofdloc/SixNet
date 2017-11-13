using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class UserUDBase
    {
        public int UserUDBaseId { get; set; }

        //Mapping to User
        public int UserId { get; set; }
        public virtual User User {get; set; }

        public int UDBaseId {get; set;}
        public virtual UDBase UDBase {get; set;}

        public DateTime LastVisit { get; set; }
    }
}
