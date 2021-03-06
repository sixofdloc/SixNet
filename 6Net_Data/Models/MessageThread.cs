﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Data.Models
{
    public class MessageThread : BaseModel
    {

        public int? MessageBaseId { get; set; }

        [ForeignKey("MessageBaseId")]
        public virtual MessageBase MessageBase { get; set; }

        public virtual ICollection<MessageBaseMessage> MessageBaseMessages { get; set; }
    }
}
