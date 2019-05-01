using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class Message : BaseModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Sent { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

    }
}
