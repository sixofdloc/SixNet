using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.ComponentModel.DataAnnotations.Schema;
using SixNet_BBS.Data.Objects;

namespace SixNet_BBS.Data
{
    public class MessageHeader
    {
        public int MessageHeaderId { get; set; }

        public int MessageThreadId { get; set; }

        public int MessageBaseId { get; set; }
        //public virtual MessageBase MessageBase { get; set; }

        public string Subject { get; set; }

        public bool Anonymous { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public int ReplyToMessage { get; set; }

        
        //public virtual ICollection<MessageHeader> Replies { get; set; }

        public DateTime Posted { get; set; }

        //[ForeignKey("MessageHeaderId")]
        public virtual MessageBody MessageBody { get; set; }
    }
}
