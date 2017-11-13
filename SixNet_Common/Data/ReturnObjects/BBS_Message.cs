using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SixNet_BBS.Data.Objects;

namespace SixNet_BBS.Data.ReturnObjects
{
    public class BBS_Message
    {
        public MessageHeader Header { get; set; }
        public MessageBody Body { get; set; }

        public BBS_Message()
        {
            Header = new MessageHeader();
            Body = new MessageBody();
        }
    }
}
