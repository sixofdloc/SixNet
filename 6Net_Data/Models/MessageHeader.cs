using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageHeader
    {
        [Key,ForeignKey("MessageBody")]
        public int Id { get; set; }
        public string Subject { get; set; }
        public bool Anonymous { get; set; }
        public DateTime Posted { get; set; }

        [ForeignKey("MessageThread")]
        public int MessageThreadId { get; set; }

        [ForeignKey("MessageBase")]
        public int MessageBaseId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }


        public MessageThread MessageThread { get; set; }
        public MessageBase MessageBase { get; set; }
        public User User { get; set; }

        public MessageBody MessageBody { get; set; }

    }
}
