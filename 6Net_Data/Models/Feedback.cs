using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class Feedback : Message
    {
        public bool Read { get; set; }
    }
}
