using System;
using FileTransferProtocols.Interfaces;
using SixNet_Comm;
using FileTransferProtocols.Protocols;
using System.IO;
using SixNet_Logger;

namespace SixNet_BBS.BBS_Classes
{
    class FileTransfers :IDataSender, IStatusRecipient
    {
        StateObject so = null;
        Punter punter = null;

        public FileTransfers(StateObject s)
        {
            so = s;
            punter = new Punter();
            punter.Initialize(this, this);
        }

        public void OnReceived(byte b)
        {
            punter.OnDataReceived(new byte[] { b }, 1);
        }

        public void Punter_Send(string filename, bool IsPrg)
        {
            try
            {
                so.AddReceiver(this.OnReceived);
                punter.Send(File.ReadAllBytes(filename), IsPrg);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in FileTransfers.Punter_Send(" + filename + "," + ((IsPrg) ? "true" : "false") + "): " + e.ToString());                
            }
            so.RemoveReceiver(this.OnReceived);
        }

        public byte[] Punter_Receive()
        {
            byte[] b = null;
            try
            {
                so.AddReceiver(this.OnReceived);
                b = punter.Receive();
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in FileTransfers.Punter_Receive(): " + e.ToString());
            }
            so.RemoveReceiver(this.OnReceived);
            return b;
        }

        public void Send(byte[] data, int count, int delay)
        {
            so.workSocket.Send(data);
        }

        public void Send(byte[] data, int count)
        {
            so.workSocket.Send(data);
        }

        public void Send(byte data)
        {
            so.workSocket.Send(new byte[] {data});
        }

        public void Send(string data)
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            so.workSocket.Send(data);
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Done(bool good)
        {
            so.RemoveReceiver(this.OnReceived);
        }

        public void Initialize(int totalbytesinfile, string filename, bool sending, string caption, Action xferAbort)
        {
            
        }

        public void MicroUpdate(string text)
        {
            
        }

        public void Update(int bytestransferred)
        {
            
        }
    }
}
