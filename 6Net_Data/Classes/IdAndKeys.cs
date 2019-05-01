using System;
using System.Collections.Generic;

namespace Net_Data.Classes
{
    public class IdAndKeys
    {
        public int Id { get; set; }
        public Dictionary<string, string> Keys { get; set; }

        public IdAndKeys()
        {
            Keys = new Dictionary<string, string>();
        }

    }
}
