using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class UDBase : TitledModel
    {
        [ForeignKey("UDBaseArea")]
        public int UDBaseAreaId { get; set; }

        public string FilePath { get; set; }

        public UDBaseArea UDBaseArea { get; set; }
    }
}
