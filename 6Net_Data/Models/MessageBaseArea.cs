using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBaseArea : TitledModel
    {
        [ForeignKey("ParentMessageBaseArea")]
        public int ParentAreaId { get; set; }

        public MessageBaseArea ParentMessageBaseArea { get; set; }
    }
}
