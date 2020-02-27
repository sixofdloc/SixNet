using System;
namespace Net_Data.Models
{
    public class UserHasReadMessage : BaseModel
    {
        public int UserId { get; set; }
        public int MessageBaseMessageId { get; set; }

        public virtual User User { get; set; }
        public virtual MessageBaseMessage MessageBaseMessage { get; set; }
    }
}
