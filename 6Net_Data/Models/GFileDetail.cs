using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class GFileDetail : TitledModel
    {
        [ForeignKey("GFileArea")]
        public int GFileAreaId { get; set; }
        public bool PETSCII { get; set; }

        public string FilePath {get; set;}

        public string Filename { get; set; }
        public int FileSizeInBytes { get; set; }

        public string Notes {get; set;}

        public DateTime Added { get; set; }

        public GFileArea GFileArea { get; set; }
        
    }
}
