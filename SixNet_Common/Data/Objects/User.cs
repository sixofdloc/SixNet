using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SixNet_BBS.Data.Objects;

namespace SixNet_BBS.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime LastDisconnection { get; set; }
        public string LastConnectionIP { get; set; }
        public int AccessLevel { get; set; }
        public string RealName { get; set; }
        public string ComputerType {get; set;}
        public string Email {get; set;}
        public virtual ICollection<UserUDBase> UserUDBases { get; set; }
        public virtual ICollection<UserMessageBase> UserMessageBases { get; set; }
        public virtual ICollection<MessageHeader> MessageHeaders { get; set; }

    }
}
