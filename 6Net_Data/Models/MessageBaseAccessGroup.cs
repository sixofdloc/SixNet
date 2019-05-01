using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBaseAccessGroup : BaseModel
    {
        [ForeignKey("MessageBase")]
        public int MessageBaseId { get; set; }

        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }

        public MessageBase MessageBase { get; set; }
        public AccessGroup AccessGroup { get; set; }
    }
}
