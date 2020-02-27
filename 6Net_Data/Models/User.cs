using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class User : BaseModel
    {
        [Index(IsUnique = true)]
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime LastDisconnection { get; set; }
        public string LastConnectionIP { get; set; }
        public string RealName { get; set; }
        public string ComputerType { get; set; }
        public string Email { get; set; }
        public string WebPage { get; set; }

        public virtual ICollection<UserAccessGroup> UserAccessGroups { get; set; }

        public virtual ICollection<CallLog> CallLogs { get; set; }

        public virtual ICollection<MessageBaseMessage> MessageBaseMessages { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<NewsItem> NewsItems { get; set; }

        public virtual ICollection<UserHasReadMessage> UserHasReadMessages { get; set; }
    }
}
