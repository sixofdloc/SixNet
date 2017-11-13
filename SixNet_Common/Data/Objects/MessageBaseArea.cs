using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class MessageBaseArea
    {
        public int MessageBaseAreaId { get; set; }

        public string Title { get; set; }
        public string LongDescription { get; set; }
        public int ParentAreaId { get; set; }
        public int AccessLevel { get; set; }

        public virtual ICollection<MessageBase> MessageBaseDetails { get; set; }

    }
}
