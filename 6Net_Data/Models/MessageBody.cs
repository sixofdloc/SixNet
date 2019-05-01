using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageBody : BaseModel
    {
        public int MessageHeaderId { get; set; }
        public string Body { get; set; }

    }
}
