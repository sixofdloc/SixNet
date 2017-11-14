using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using SixNet_Logger;

namespace SixNet_Comm
{
    public class Server
    {
        private readonly int _portNumber;
        protected int maxSockets;
        protected int sockCount = 0;
        private int convID;
        private Timer lostTimer;
        private const int numThreads = 1;
        private const int timerTimeout = 1000;
        private const int timeoutMinutes = 3;
        private bool ShuttingDown = false;
        protected string title;
        protected Hashtable connectedHT = new Hashtable();
        public List<StateObject> connectedSocks;
        protected string msgConnect;

        //Thread signal.
        private ManualResetEvent allDone = new ManualResetEvent(false);
        private Thread[] serverThread = new Thread[numThreads];
        private AutoResetEvent[] threadEnd = new AutoResetEvent[numThreads];

       

       // private MainForm parentForm;

        private Action<StateObject> OnConnect;
        private Action<StateObject> OnDisconnect;

        public Server(int port, string title, string attr,Action<StateObject> onconnect, Action<StateObject>  ondisconnect)
        {
            Console.WriteLine("Server initialized on port: " + port.ToString());
            _portNumber = port;
            this.title = title;
            this.maxSockets =10000;
            //this.parentForm = parentform;
            OnConnect = onconnect;
            OnDisconnect = ondisconnect;
            connectedSocks = new List<StateObject>(this.maxSockets);// ArrayList(this.maxSockets);
        }

/// 
/// Description: Start the threads to listen to the port and process
/// messages.0  
/// 

        public void Start()
        {
            // Clear the thread end events
            for (int lcv = 0; lcv < numThreads; lcv++)
            threadEnd[lcv] = new AutoResetEvent(false);

            Console.WriteLine("Server started");

            ThreadStart threadStart1 = new ThreadStart(StartListening);
            serverThread[0] = new Thread(threadStart1)
            {
                IsBackground = true
            };
            serverThread[0].Start();

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate = new TimerCallback(this.CheckSockets);
            // Create a timer that waits one minute, then invokes every second
            lostTimer = new Timer(timerDelegate, null,60000, Server.timerTimeout);
        }

/// 
/// Description: Check for dormant sockets and close them.
/// 
/// <param name="eventState">Required parameter for a timer call back
/// method.</param>
private void CheckSockets(object eventState)
{
    lostTimer.Change(System.Threading.Timeout.Infinite,
    System.Threading.Timeout.Infinite);
    try
    {
        foreach (StateObject state in connectedSocks)
        {
            if (state.workSocket == null)
            {	// Remove invalid state object
                //Monitor.Enter(connectedSocks);
                //if (connectedSocks.Contains(state))
                //{
                //    connectedSocks.Remove(state);
                //    Interlocked.Decrement(ref sockCount);
                //}
                //Monitor.Exit(connectedSocks);
                OnDisconnect(state);
                RemoveSocket(state);

            

            } else {

                if (!state.workSocket.IsConnected())
                {
                    OnDisconnect(state);
                    RemoveSocket(state);
                    //Monitor.Enter(connectedSocks);
                    //connectedSocks.Remove(state);
                    //Interlocked.Decrement(ref sockCount);
                    //Monitor.Exit(connectedSocks);
                }
                
                if (DateTime.Now.AddTicks(-state.TimeStamp.Ticks).Minute > timeoutMinutes)
                {
        //            RemoveSocket(state);
                    //TODO: Put this back, but tie to idle timer.

                }
            }
        }
    }
    catch (Exception)
    {
        //What, nothing?
    }
    finally
    {
        lostTimer.Change(Server.timerTimeout, Server.timerTimeout);
    }
}

/// 
/// Decription: Stop the threads for the port listener.
/// 
    public void Stop()
    {
        int lcv;
        lostTimer.Dispose();
        lostTimer = null;
        Console.WriteLine("Server stopped");

        for (lcv = 0; lcv < numThreads; lcv++)
        {
            if (!serverThread[lcv].IsAlive) threadEnd[lcv].Set();	// Set event if thread is already dead
        }
        ShuttingDown = true;
        // Create a connection to the port to unblock the listener thread
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, _portNumber);
        sock.Connect(endPoint);
        sock.Close();
        sock = null;

        // Check thread end events and wait for up to 5 seconds.
        for (lcv = 0; lcv < numThreads; lcv++)
            threadEnd[lcv].WaitOne(5000, false);
    }

/// 
/// Decription: Open a listener socket and wait for a connection.
/// 
    private void StartListening()
    {
        // Establish the local endpoint for the socket.
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _portNumber);
        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(1000);
            while (!ShuttingDown)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();
                // Start an asynchronous socket to listen for connections.

                listener.BeginAccept(new AsyncCallback(this.AcceptCallback), listener);

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }
        }
        catch (Exception e)
        {
            LoggingAPI.LogEntry("Exception in Server.StartListening: " + e.Message);
            threadEnd[0].Set();
        }
    }

/// 
/// Decription: Call back method to accept new connections.
/// 
/// <param name="ar">Status of an asynchronous operation.</param>
    private void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.
	    allDone.Set();
        // Get the socket that handles the client request.
	    Socket listener = (Socket) ar.AsyncState;
	    Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject()
            {
                workSocket = handler,
                TimeStamp = DateTime.Now
            };
            try
	    {
	        Interlocked.Increment(ref sockCount);
	        Monitor.Enter(connectedSocks);
            connectedSocks.Add(state);
            Monitor.Exit(connectedSocks);
            //Update parent form
            OnConnect(state);
           
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(this.ReadCallback), state);
            if (sockCount > this.maxSockets)
            {
                RemoveSocket(state);
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
                handler = null;
                state = null;
            }
        }
        catch (SocketException es)
        {
            LoggingAPI.LogEntry("SocketException in Server.AcceptCallback: " + es.Message);
            RemoveSocket(state);
        }
        catch (Exception e)
        {
            LoggingAPI.LogEntry("Exception in Server.AcceptCallback: " + e.Message);
            RemoveSocket(state);
        }
    }


/// 
/// Decription: Call back method to handle incoming data.
/// 
/// <param name="ar">Status of an asynchronous operation.</param>
    protected void ReadCallback(IAsyncResult ar)
    {
        //String content = String.Empty;
        // Retrieve the state object and the handler socket
        // from the async state object.
        StateObject state = (StateObject) ar.AsyncState;
        Socket handler = state.workSocket;
        try
        {
            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                Monitor.Enter(state);
                //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                //Console.WriteLine("Server read: " + Encoding.ASCII.GetString(state.buffer,0,bytesRead));
                //foreach (char c in Encoding.ASCII.GetString(state.buffer, 0, bytesRead))
                //{
                //    state.Received(c);
                //}
                for (int i = 0; i<bytesRead;i++)
                {
                    state.Received(state.buffer[i]);
                }
                Monitor.Exit(state);
                //content = state.sb.ToString();
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(this.ReadCallback), state);
            } else { //Disconnected
                RemoveSocket(state);
            }
        }
        catch (System.Net.Sockets.SocketException es)
        {
            RemoveSocket(state);
            OnDisconnect(state);
            if (es.ErrorCode != 64)
            {
                Console.WriteLine( string.Format("ReadCallback Socket Exception: {0}, {1}.",es.ErrorCode, es.ToString()));
            }
        }
        catch (Exception e)
        {
            RemoveSocket(state);
            OnDisconnect(state);
            if (e.GetType().FullName != "System.ObjectDisposedException")
            {
                Console.WriteLine(string.Format("ReadCallback Exception: {0}.", e.ToString()));
            }
        }
    }

/// 
/// Decription: Send the given data string to the given socket.
/// 
/// <param name="sock">Socket to send data to.</param>
/// <param name="data">The string containing the data to send.</param>
    public void Send(Socket sock, string data)
    {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        if (byteData.Length > 0) sock.BeginSend(byteData, 0, byteData.Length, 0,new AsyncCallback(this.SendCallback), sock);
    }

/// 
/// Decription: Call back method to handle outgoing data.
/// 
/// <param name="ar">Status of an asynchronous operation.</param>
    protected void SendCallback(IAsyncResult ar)
    {
        // Retrieve the socket from the async state object.
        Socket handler = (Socket) ar.AsyncState;
        try
        {
            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
        }
        catch (Exception e)
        {
            //Nothing?
            LoggingAPI.LogEntry("Exception in Server.SendCallback: " + e.Message);
        }
    }

        public void Disconnect(StateObject so)
        {
            try {
        //    so.workSocket.BeginDisconnect(true, new AsyncCallback(this.DisconnectCallback), so);
        //}


        //public void DisconnectCallback(IAsyncResult ar)
        //{
        //    StateObject so = (StateObject)ar.AsyncState;
        //    try
        //    {
        //        //parentForm.DropConnection(so);
        //        so.workSocket.EndDisconnect(ar);
                so.Disconnect();
                so.workSocket = null;
                OnDisconnect(so);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in Server.DisconnectCallback: " + e.Message);
            }
        }

/// 
// Description: Find on socket using the identifier as the key.
/// 
/// <param name="id">The identifier key associated with a socket.</param>

    public Socket FindID(string id)
    {
        Socket sock = null;
        Monitor.Enter(connectedHT);
        if (connectedHT.ContainsKey(id)) sock = (Socket) connectedHT[id];
        Monitor.Exit(connectedHT);
        return sock;
    }

/// 
/// Description: Place the given socket in the connected list.
///
/// Notes:
/// 1) Get the Member ID from the command
/// 2) Add the socket to the connected socket list with its Member ID
/// 
/// <param name="state">The state object containing the socket info.</param>
/// <param name="command">The XML-based message containing the ID that
/// needs to be parsed.</param>
/// <param name="content">Not used in the base method.</param>



    //virtual protected bool StuffList(StateObject state, string command,ref string content)
    //{
    //    Socket sock = state.workSocket;
    //    string hostID = ReadXML(command, Server.msgConnect, this.connectAttr);

    //    if (hostID != null)
    //    {
    //        state.id = hostID;
    //        if (hostID == Server.webServer)
    //        {
    //            Console.WriteLine(string.Format("Host control socket connected {0}!",this.title));
    //            return true;
    //        }

    //        // Add to connected list
    //        Monitor.Enter(connectedHT);
    //        if (connectedHT.ContainsKey(hostID))
    //        {
    //            object val = connectedHT[hostID];
    //            connectedHT[hostID] = sock;
    //            connectedHT.Add(sock, val);
    //            Console.WriteLine(string.Format("Socket found in Hashtable!",this.title));
    //        } else {
    //            connectedHT.Add(hostID, sock);
    //            connectedHT.Add(sock, hostID);
    //            Console.WriteLine(string.Format("Socket not found, adding a new socket to hashtable!",this.title));
    //        }
    //        Monitor.Exit(connectedHT);
    //        Console.WriteLine(string.Format("Socket was moved to connected {0} list!",this.title));
    //        return true;
    //    }
    //    return false;
    //}

/// 
/// Description: Remove the socket contained in the given state object
/// from the connected array list and hash table, then close the socket.
/// 
/// <param name="state">The StateObject containing the specific socket
/// to remove from the connected array list and hash table.</param>
    virtual protected void RemoveSocket(StateObject state)
    {
        Socket sock = state.workSocket;
        Monitor.Enter(connectedSocks);
        if (connectedSocks.Contains(state))
        {
            connectedSocks.Remove(state);
            Interlocked.Decrement(ref sockCount);
        }
        Monitor.Exit(connectedSocks);
        Monitor.Enter(connectedHT);

        if ((sock != null) && (connectedHT.ContainsKey(sock)))
        {
            object sockTemp = connectedHT[sock];
            if (connectedHT.ContainsKey(sockTemp))
            {
                if (connectedHT.ContainsKey(connectedHT[sockTemp]))
                {
                    connectedHT.Remove(sock);
                    if (sock.Equals(connectedHT[sockTemp]))
                    {
                        connectedHT.Remove(sockTemp);
                    } else {
                        object val, key = sockTemp;
                        while (true)
                        {
                            val = connectedHT[key];
                            if (sock.Equals(val))
                            {
                                connectedHT[key] = sockTemp;
                                break;
                            }   
                            else if (connectedHT.ContainsKey(val))
                            key = val;
                            else	// The chain is broken
                            break;
                        }
                    }
                } else {
                    Console.WriteLine(string.Format("Socket is not in the {0} connected hash table!",	this.title));
                }
            }
        }
        Monitor.Exit(connectedHT);

        if (sock != null)
        {
            //if (sock.Connected)
            //    sock.Shutdown(SocketShutdown.Both);
            sock.Close();
            sock = null;
            state.workSocket = null;
            state = null;

        }
    }

}

}
