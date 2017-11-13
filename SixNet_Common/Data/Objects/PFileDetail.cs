using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class PFileDetail
    {
        public int PFileDetailId { get; set; }

        public int PFileNumber { get; set; } //This pfile's position in the list

        public int ParentAreaId { get; set; } // -1 == in the main area

        public string Filename { get; set; } //relative to base UD path
        public string Description { get; set; }
        public DateTime Added { get; set; }

    }
}
