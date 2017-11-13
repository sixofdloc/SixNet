using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SixNet_BBS.Data.Objects
{
    public class MessageBody
    {
        public int MessageBodyId { get; set; }

        public int MessageHeaderId { get; set; }
       
        public string Body { get; set; }
    }
}
