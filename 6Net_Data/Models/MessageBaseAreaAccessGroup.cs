using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBaseAreaAccessGroup : BaseModel
    {
        [ForeignKey("MessageBaseArea")]
        public int MessageBaseAreaId { get; set; }

        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }

        public MessageBaseArea MessageBaseArea { get; set; }
        public AccessGroup AccessGroup { get; set; }
    }
}
