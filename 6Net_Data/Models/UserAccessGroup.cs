using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class UserAccessGroup : BaseModel
    {
        [ForeignKey("AccessGroup")]
        public int AccessGroupId { get; set; }
       
         [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
        public AccessGroup AccessGroup { get; set; }
    }
}
