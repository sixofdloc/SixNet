using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SixNet;
using SixNet.BBS;

namespace Test_PFile
{
   public class PFile
    {
       public void Main(BBS Host_System)
       {
           for (int i = 0; i < 10; i++)
           {
               Host_System.WriteLine("Hello World: " + i.ToString());
           }
           Host_System.WriteLine("Press Any Key To Continue");
           Host_System.GetChar();
       }

    }
}
