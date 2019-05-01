using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class UDBaseArea : TitledModel
    {
        [ForeignKey("ParentUDBaseArea")]
        public int ParentAreaId { get; set; }

        public UDBaseArea ParentUDBaseArea { get; set; }
    }
}
