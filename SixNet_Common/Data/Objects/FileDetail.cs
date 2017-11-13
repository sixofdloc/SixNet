using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.ComponentModel.DataAnnotations.Schema;

namespace SixNet_BBS.Data.Objects
{
    public class FileDetail
    {
        public int FileDetailId { get; set; }

        public int UDBaseID { get; set; }
        public virtual UDBase UDBase { get; set; }

        public int UploaderID { get; set; }

  //      [ForeignKey("UploaderID")]
        public virtual User Uploader { get; set; }

        public string Filename { get; set; } //relative to base UD path
        public string Description { get; set; }
        public int FileSizeInBytes { get; set; }
        public DateTime Uploaded { get; set; }
    }
}
