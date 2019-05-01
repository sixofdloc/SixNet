using System;
using System.Collections.Generic;
using Net_BBS.BBS_Core;

namespace Net_BBS.Interfaces
{
    public interface IBBSHost
    {
        List<BBS> GetAllNodes();
    }
}
