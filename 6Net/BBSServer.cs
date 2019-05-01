using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Net_BBS.BBS_Core;
using Net_BBS.Interfaces;
using Net_Comm.Classes;
using Net_Logger;

namespace Net
{
    public class BBSServer : IBBSHost
    {
        Server server;
        public List<BBS> bbs_instances;
        Thread bbsServerThread;

        private readonly string  _connectionString = "";

        public BBSServer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Start()
        {
            //Spawn main BBS Thread
            bbs_instances = new List<BBS>();
            bbsServerThread = new Thread(new ThreadStart(MainThread));            
            bbsServerThread.Start();

        }

        public void Stop()
        {
            //Stop main BBS Thread
            server.Stop();
            bbsServerThread.Abort();
        }

        public void MainThread()
        {
            server = new Server(6969,"Client Session","",AddConnection, DropConnection);
            server.Start();
        }

        public void AddConnection(StateObject so)
        {
            try
            {
                StartBBS(so);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in AddConnection: " + e.Message);
            }
        }

        public void DropConnection(StateObject so)
        {
            BBS bbs = bbs_instances.FirstOrDefault(p => p.State_Object.Equals(so));
            if (bbs != null)
            {
                bbs_instances.Remove(bbs);
            }
        }

        private void StartBBS(StateObject so)
        {
            BBS bbs = new BBS(this, so, _connectionString);
            if (bbs_instances == null) bbs_instances = new List<BBS>();
            bbs_instances.Add(bbs);
            Thread thread = new Thread(() => bbs.Go());
            thread.Start();

        }

        public List<BBS> GetAllNodes()
        {
            throw new NotImplementedException();
        }
    }
}
