using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class PFileArea
    {
        public int PFileAreaId { get; set; }

        public string Title { get; set; }
        public string LongDescription { get; set; }
        public int ParentAreaId { get; set; }
        public int AccessLevel { get; set; }

        public virtual ICollection<PFileDetail> PFileDetails { get; set; }

    }
}
