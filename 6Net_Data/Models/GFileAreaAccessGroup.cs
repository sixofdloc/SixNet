using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class GFileAreaAccessGroup : BaseModel
    {
        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }
        [ForeignKey("GFileArea")]
        public int GFileAreaId { get; set; }

        public GFileArea GFileArea { get; set; }
        public AccessGroup AccessGroup { get; set; }

    }
}
