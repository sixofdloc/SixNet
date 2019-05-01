using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class CallLog : BaseModel
    {

        [ForeignKey("User") ]
        public int UserId { get; set; }

        public DateTime Connected { get; set; }
        public DateTime Disconnected { get; set; }

        public virtual User User { get; set; }
    }
}
