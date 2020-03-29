using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Net_Logger;

namespace Net_Comm.Classes
{
        public class Server
        {
            private readonly int _portNumber;
            protected int maxSockets;
            protected int sockCount = 0;
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



            private Action<StateObject> OnConnect;
            private Action<StateObject> OnDisconnect;

            public Server(int port, string title, string attr, Action<StateObject> onconnect, Action<StateObject> ondisconnect)
            {
                LoggingAPI.Information("Server initialized on port: {port}",port);
                _portNumber = port;
                this.title = title;
                this.maxSockets = 10000;
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

                LoggingAPI.Information("Server started");

                ThreadStart threadStart1 = new ThreadStart(StartListening);
                serverThread[0] = new Thread(threadStart1)
                {
                    IsBackground = true
                };
                serverThread[0].Start();

                // Create the delegate that invokes methods for the timer.
                TimerCallback timerDelegate = new TimerCallback(this.CheckSockets);
                // Create a timer that waits one minute, then invokes every second
                lostTimer = new Timer(timerDelegate, null, 60000, Server.timerTimeout);
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
                        {   // Remove invalid state object
                            OnDisconnect(state);
                            RemoveSocket(state);
                        }
                        else
                        {
                            if (!state.workSocket.IsConnected())
                            {
                                OnDisconnect(state);
                                RemoveSocket(state);
                            }

                            if (DateTime.Now.AddTicks(-state.TimeStamp.Ticks).Minute > timeoutMinutes)
                            {
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
                LoggingAPI.Information("Server stopped");

                for (lcv = 0; lcv < numThreads; lcv++)
                {
                    if (!serverThread[lcv].IsAlive) threadEnd[lcv].Set();   // Set event if thread is already dead
                }
                ShuttingDown = true;
                // Create a connection to the port to unblock the listener thread
                //I think this can go now.

                //            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, _portNumber);
                //            sock.Connect(endPoint);
                //            sock.Disconnect(false);
                //            sock.Close();
                //           sock = null;

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
                catch (Exception exception)
                {
                    LoggingAPI.Exception(exception,new { });
                    threadEnd[0].Set();
                }
            }

            /// 
            /// Decription: Call back method to accept new connections.
            /// 
            /// <param name="asyncResult">Status of an asynchronous operation.</param>
            private void AcceptCallback(IAsyncResult asyncResult)
            {
                // Signal the main thread to continue.
                allDone.Set();
                // Get the socket that handles the client request.
                Socket listener = (Socket)asyncResult.AsyncState;
                Socket handler = listener.EndAccept(asyncResult);

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
                    state.connected = true;
                state.RemoteAddress = state.workSocket.RemoteEndPoint.ToString();
                    OnConnect(state);

                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(this.ReadCallback), state);
                    if (sockCount > this.maxSockets)
                    {
                        RemoveSocket(state);
                        //handler.Shutdown(SocketShutdown.Both);
                        //handler.Close();
                        handler = null;
                        state = null;
                    }
                }
                catch (SocketException socketException)
                {
                    RemoveSocket(state);
                    LoggingAPI.Exception(socketException,new { asyncResult, state });
                }
                catch (Exception exception)
                {
                    RemoveSocket(state);
                    LoggingAPI.Exception(exception, new { asyncResult, state });
                }
            }


            /// 
            /// Decription: Call back method to handle incoming data.
            /// 
            /// <param name="asyncResult">Status of an asynchronous operation.</param>
            protected void ReadCallback(IAsyncResult asyncResult)
            {
                //String content = String.Empty;
                // Retrieve the state object and the handler socket
                // from the async state object.
                StateObject state = (StateObject)asyncResult.AsyncState;
                Socket handler = state.workSocket;
            try
            {
                // Read data from the client socket.
                int bytesRead = handler.EndReceive(asyncResult);

                if (bytesRead > 0)
                {
                    Monitor.Enter(state);
                    for (int i = 0; i < bytesRead; i++)
                    {
                        state.Received(state.buffer[i]);
                    }
                    Monitor.Exit(state);
                    //content = state.sb.ToString();
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(this.ReadCallback), state);
                }
                else
                { //Disconnected
                    RemoveSocket(state);
                }
            }
            catch (System.Net.Sockets.SocketException socketException)
            {
                RemoveSocket(state);
                OnDisconnect(state);
                if (socketException.ErrorCode == 10057 || socketException.NativeErrorCode == 10054)
                {
                    LoggingAPI.Information(state.RemoteAddress +": Socket Disconnected");
                }
                else
                {
                    if (socketException.ErrorCode != 64)
                    {
                        LoggingAPI.Exception(socketException,new { asyncResult });
                    }
                }
            }
            catch (Exception exception)
            {
                RemoveSocket(state);
                OnDisconnect(state);
                if (exception.GetType() != typeof(System.ObjectDisposedException))
                {
                    LoggingAPI.Exception(exception, new { asyncResult });
                }
            }
            }

            /// 
            /// Decription: Send the given data string to the given socket.
            /// 
            /// <param name="socket">Socket to send data to.</param>
            /// <param name="data">The string containing the data to send.</param>
            public void Send(Socket socket, string data)
            {
                try
                {
                    // Convert the string data to byte data using ASCII encoding.
                    byte[] byteData = Encoding.ASCII.GetBytes(data);

                    // Begin sending the data to the remote device.
                    if (byteData.Length > 0) socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(this.SendCallback), socket);
                }
                catch (Exception exception)
                {
                    LoggingAPI.Exception(exception,new { socket, data });
                }
            }

            /// 
            /// Decription: Call back method to handle outgoing data.
            /// 
            /// <param name="asyncResult">Status of an asynchronous operation.</param>
            protected void SendCallback(IAsyncResult asyncResult)
            {
                // Retrieve the socket from the async state object.
                Socket handler = (Socket)asyncResult.AsyncState;
                try
                {
                    // Complete sending the data to the remote device.
                    int bytesSent = handler.EndSend(asyncResult);
                }
                catch (Exception exception)
                {
                    LoggingAPI.Exception(exception,new { asyncResult });
                }
            }

            public void Disconnect(StateObject stateObject)
            {
                try
                {
                    stateObject.Disconnect();
                    stateObject.workSocket = null;
                    OnDisconnect(stateObject);
                }
                catch (Exception exception)
                {
                    LoggingAPI.Exception(exception,new { stateObject });
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
                if (connectedHT.ContainsKey(id)) sock = (Socket)connectedHT[id];
                Monitor.Exit(connectedHT);
                return sock;
            }

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
                            }
                            else
                            {
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
                                    else    // The chain is broken
                                        break;
                                }
                            }
                        }
                        else
                        {
                            //Nada
                        }
                    }
                }
                Monitor.Exit(connectedHT);

                if (sock != null)
                {
                    sock.Close();
                    sock = null;
                    state.workSocket = null;
                    state = null;

                }
            }

        }
}
