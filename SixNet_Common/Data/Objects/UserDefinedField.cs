using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_BBS.Data.Objects
{
    public class UserDefinedField
    {
        public int UserDefinedFieldId { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public string FieldValue { get; set; }
    }
}
