using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class Field : BaseModel
    {
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public string FieldName { get; set; }
        public string FieldContents { get; set; }

        public virtual User User { get; set; }
    }
}
