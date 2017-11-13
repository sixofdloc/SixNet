using System.Collections.Generic;

namespace SixNet_BBS.Interfaces
{
    public interface IBBSHost
    {
         List<BBS> GetAllNodes();
    }
}
