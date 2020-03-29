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
        private readonly string _description;
        private readonly string  _connectionString;
        private readonly int _port;

        public BBSServer(string connectionString, int port, string description)
        {
            _connectionString = connectionString;
            _description = description;
            _port = port;
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
            server = new Server(_port,_description,"",this.AddConnection, this.DropConnection);
            server.Start();
        }

        public void AddConnection(StateObject stateObject)
        {
            try
            {
                StartBBS(stateObject);
            }
            catch (Exception exception)
            {
                LoggingAPI.Exception(exception, new { stateObject }); 
            }
        }

        public void DropConnection(StateObject so)
        {
            BBS bbs = bbs_instances.FirstOrDefault(p => p._stateObject.Equals(so));
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
