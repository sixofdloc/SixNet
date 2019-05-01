using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class PFileDetail : TitledModel
    {
        [ForeignKey("PFileArea")]
        public int PFileAreaId { get; set; }

        public string Filename { get; set; }
        public int FileSizeInBytes { get; set; }

        public DateTime Added { get; set; }

        public PFileArea PFileArea { get; set; }
    }
}
