namespace SixNet_BBS_Data
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
