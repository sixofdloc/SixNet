using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class Message : BaseModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Posted { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
