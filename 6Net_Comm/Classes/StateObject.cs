using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Net_Comm.Classes
{
    public class StateObject
    {

        private List<Action<byte>> Receivers { get; set; }
        private List<Action> Disconnect_Handlers { get; set; }
        //public ChatForm chatForm = null;

        public bool connected = false;  // ID received flag
        public Socket workSocket = null;    // Client socket.
        public Socket partnerSocket = null; // Partner socket.
        public const int BufferSize = 1024; // Size of receive buffer.
        public byte[] buffer = new byte[BufferSize];// Receive buffer.
        //public StringBuilder sb = new StringBuilder();//Received data String.
        public string id = String.Empty;    // Host or conversation ID
        public DateTime TimeStamp;

        public string RemoteAddress;

        public StateObject()
        {
            Receivers = new List<Action<byte>>();
            Disconnect_Handlers = new List<Action>();
            RemoteAddress = "";
        }

        public void Received(byte b)
        {
            foreach (Action<byte> Receiver in Receivers)
            {
                Receiver(b);
            }
        }

        public void Disconnect()
        {
            if (workSocket != null)
            {
                connected = false;
                workSocket.Disconnect(false);
                workSocket = null;
                RemoteAddress = "";
            }
            foreach (Action Disconnect_Handler in Disconnect_Handlers)
            {
                Disconnect_Handler();
            }
        }

        public Action<byte> GetCurrentReceiver()
        {
            return Receivers.First(p => true);
        }

        public void SoloReceiver(Action<byte> receiver)
        {
            Receivers.RemoveAll(p => true);
            AddReceiver(receiver);
        }

        public void AddReceiver(Action<byte> receiver)
        {
            Receivers.Add(receiver);
        }

        public void RemoveReceiver(Action<byte> receiver)
        {
            Receivers.Remove(receiver);
        }

        public void AddDisconnectHandler(Action handler)
        {
            Disconnect_Handlers.Add(handler);
        }

        public void RemoveDisconnectHandler(Action handler)
        {
            Disconnect_Handlers.Remove(handler);
        }

    }
}
