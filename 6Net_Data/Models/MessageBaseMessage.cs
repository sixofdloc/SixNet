using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBaseMessage : Message
    {
        public bool Anonymous { get; set; }
        public int? MessageThreadId { get; set; }
        public int? MessageBaseId { get; set; }


        [ForeignKey("MessageThreadId")]
        public virtual MessageThread MessageThread { get; set; }

        [ForeignKey("MessageBaseId")]
        public virtual MessageBase MessageBase { get; set; }



    }
}
