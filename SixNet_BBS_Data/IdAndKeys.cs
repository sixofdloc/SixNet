using System.Collections.Generic;

namespace SixNet_BBS_Data
{
    public class IdAndKeys
    {
        public int Id { get; set; }
        public Dictionary<string,string> Keys { get; set; }

        public IdAndKeys()
        {
            Keys = new Dictionary<string, string>();
        }

    }
}
