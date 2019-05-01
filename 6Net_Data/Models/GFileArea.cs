using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class GFileArea : TitledModel
    {
        [ForeignKey("ParentGFileArea")]
        public int? ParentAreaId { get; set; }

        public ICollection<GFileAreaAccessGroup> GFileAreaAccessGroups { get; set; }

        public ICollection<GFileArea> ChildAreas { get; set; }

        public GFileArea ParentGFileArea { get; set; }
    }
}
