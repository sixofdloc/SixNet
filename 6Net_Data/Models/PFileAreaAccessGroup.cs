using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class PFileAreaAccessGroup : BaseModel
    {
        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }
        [ForeignKey("PFileArea")]
        public int PFileAreaId { get; set; }

        public PFileArea PFileArea { get; set; }
        public AccessGroup AccessGroup { get; set; }

    }
}
