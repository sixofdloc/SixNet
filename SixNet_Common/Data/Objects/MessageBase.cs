using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SixNet_BBS.Data.Objects;

namespace SixNet_BBS.Data
{
    public class MessageBase
    {
        public int MessageBaseId { get; set; }

        public string Title { get; set; }
        public string LongDescription { get; set; }
        public int ParentArea { get; set; }
        public int AccessLevel { get; set; }

        public virtual ICollection<MessageHeader> MessageHeaders { get; set; }
        public virtual ICollection<UserMessageBase> UserMessageBases { get; set; }


    }
}
