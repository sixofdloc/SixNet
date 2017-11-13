using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class UDBase
    {
        public int UDBaseId { get; set; }

        public string Title { get; set; }
        public string LongDescription { get; set; }

        public virtual ICollection<FileDetail> FileDetails { get; set; }
        public virtual ICollection<UserUDBase> UserUDBases { get; set; }


    }
}
