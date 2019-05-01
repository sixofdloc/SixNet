using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageThread : BaseModel
    {
        [ForeignKey("MessageBase")]
        public int MessageBaseId { get; set; }

        [ForeignKey("InitialMessageHeader")]
        public int InitialMessageHeaderId { get; set; }

        public MessageBase MessageBase { get; set; }
        public MessageHeader InitialMessageHeader { get; set; }

        public ICollection<MessageHeader> MessageHeaders { get; set; }
    }
}
