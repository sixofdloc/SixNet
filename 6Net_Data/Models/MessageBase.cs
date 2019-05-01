using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBase : TitledModel
    {
        [ForeignKey("MessageBaseArea")]
        public int MessageBaseAreaId {get; set;}

        public ICollection<MessageHeader> MessageHeaders { get; set; }

        public MessageBaseArea MessageBaseArea { get; set; }

    }
}
