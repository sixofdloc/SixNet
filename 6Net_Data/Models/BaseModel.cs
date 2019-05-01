using System;
using System.ComponentModel.DataAnnotations;

namespace Net_Data.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

    }
}
