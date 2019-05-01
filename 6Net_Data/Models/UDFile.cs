using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class UDFile : BaseModel
    {
        public string Filename { get; set; }
        public int Filesize { get; set; }
        public DateTime Uploaded { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }

        [ForeignKey("UDBase")]
        public int UDBaseId { get; set; }

        [ForeignKey("Uploader")]
        public int UploaderId { get; set; }

        public User Uploader { get; set; }
        public UDBase UDBase { get; set; }
    }
}
