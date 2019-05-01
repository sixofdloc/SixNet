using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class UDBaseAccessGroup : BaseModel
    {
        [ForeignKey("UDBase")]
        public int UDBaseId { get; set; }

        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }

        public UDBase UDBase { get; set; }
        public AccessGroup AccessGroup { get; set; }
    }
}
