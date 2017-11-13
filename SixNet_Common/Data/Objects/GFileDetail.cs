using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class GFileDetail
    {
        public int GFileDetailId { get; set; }

        public int GFileAreaId { get; set; } //-1 is in main area

        public string Filename { get; set; } //relative to base UD path
        public string DisplayFilename { get; set; }
        public string Description { get; set; }
        public int FileSizeInBytes { get; set; }
        public DateTime Added { get; set; }
        public bool PETSCII { get; set; }

    }
}
