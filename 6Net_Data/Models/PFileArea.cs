using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class PFileArea : TitledModel
    {
        [ForeignKey("ParentPFileArea")]
        public int? ParentAreaId { get; set; }

        public ICollection<PFileAreaAccessGroup> PFileAreaAccessGroups { get; set; }

        public ICollection<PFileArea> ChildAreas { get; set; }
        public ICollection<PFileDetail> PFiles { get; set; }

        public PFileArea ParentPFileArea { get; set; }
    }
}
