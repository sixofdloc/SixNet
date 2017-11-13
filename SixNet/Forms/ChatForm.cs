using System;
using System.Drawing;
using System.Windows.Forms;
using SixNet_Comm;
using SixNet_BBS;
using SixNet_BBS_Data;

namespace SixNet
{
    public partial class ChatForm : Form
    {
        delegate void SetTextCallback(byte c);
        delegate void EmptyCallback();

        public BBS Bbs { get; set; }
        public Server server { get; set; }

        public Action<byte> OldReceiver { get; set; }

        delegate void SetUserDetailsCallback(string username, string access);

        private readonly DataInterface _dataInterface;

        public ChatForm(DataInterface dataInterface)
        {
            InitializeComponent();
            _dataInterface = dataInterface;
        }

        public void ShowForm()
        {
            if (this.InvokeRequired)
            {
                EmptyCallback d = new EmptyCallback(ShowForm);
                this.Invoke(d,null);
            }
            else
            {
                this.Show();
                MonitorTimer.Enabled = true;
            }
        }

        private string currentline = "";

        public void Receive(byte c)
        {
            if (this.tbDisplay.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Receive);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                c = (byte)(Bbs.TerminalType.TranslateFromTerminal((char)c));
                Bbs.Write("~c7"+(char)c);
                if ((c.Equals('\r') || (c.Equals('\n') || c.Equals(13))))
                {
                    if (currentline != "")
                    {
                        AppendText(lblUsername.Text +": ", Color.Yellow, true);
                        AppendText(currentline + "\r\n", Color.Yellow, false);
                        currentline = "";
                    }
                }
                else
                {
                    currentline += (char)c;
                }
            }
            //textBox1.Text += str;
        }

        private void OutputForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.Disconnect(Bbs.State_Object);
            HandleDisconnect();
        }

        private void HandleDisconnect()
        {
            AppendText("\r\nClient Disconnected", Color.Red,true);
            //tbDisplay.Text += "Client Disconnected.";
            tbEntry.Enabled = false;
            btSend.Enabled = false;
            btSendFile.Enabled = false;
            btEndChat.Enabled = false;
            btKickUser.Enabled = false;
           
        }

        private void MonitorTimer_Tick(object sender, EventArgs e)
        {
            if ((Bbs.State_Object.workSocket == null) ||(!Bbs.State_Object.connected))
            {
                MonitorTimer.Enabled = false;
                HandleDisconnect();
            }
        }

        public void AppendText( string text, Color color, bool bold)
        {
            tbDisplay.SelectionStart = tbDisplay.TextLength;
            tbDisplay.SelectionLength = 0;

            tbDisplay.SelectionColor = color;
            if (bold)
            {
                tbDisplay.SelectionFont = new Font(tbDisplay.SelectionFont, FontStyle.Bold);
            }
            else
            {
                tbDisplay.SelectionFont = new Font(tbDisplay.SelectionFont, FontStyle.Regular);
            }
            tbDisplay.AppendText(text);
            tbDisplay.SelectionColor = tbDisplay.ForeColor;
            
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            Bbs.WriteLine("~l1~c1" + tbEntry.Text);
            AppendText("\r\nSysop: ", Color.White, true);
            AppendText(tbEntry.Text + "\r\n",Color.White,false);
            tbEntry.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bbs.State_Object.SoloReceiver(OldReceiver);
            Bbs.Receive((byte)'\r');
            Bbs.WriteLine("~l1~l1~d2Leaving Chat with SysOp");
            btKickUser.Enabled = false;
            btEndChat.Enabled = false;
            btSend.Enabled = false;
            btSendFile.Enabled = false;
            tbEntry.Enabled = false;
            //this.Close();
        }

        public void StartChat(BBS Bbs, Server serv)
        {
            this.RunInNewThread(false);
            //this.so = so2;
            this.Bbs = Bbs;
            this.server = serv;
            //so.AddReceiver(of.Receive);
            this.Bbs.Write("~s1~c1Entering chat with sysop~l2");
            OldReceiver = this.Bbs.State_Object.GetCurrentReceiver();
            this.Bbs.State_Object.SoloReceiver(Receive);
            SixNet_BBS_Data.AccessGroup ag = _dataInterface.GetAccessGroup(this.Bbs.CurrentUser.AccessLevel);
            SetUserDetails(this.Bbs.CurrentUser.Username, ag.AccessGroupNumber.ToString()+", "+ ag.Description);
        }

        public void SetUserDetails(string username, string access){
            if (this.InvokeRequired)
            {
                SetUserDetailsCallback d = new SetUserDetailsCallback(SetUserDetails);
                this.Invoke(d,new object[]{username,access});
            }
            else
            {
                lblUsername.Text = username;
                lblAccess.Text = access;
            }

        }

        private void tbEntry_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbEntry_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                SendMessage();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSendFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bbs.SendFile(openFileDialog1.FileName, false);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bbs.SendFile(openFileDialog1.FileName, true);
            }
        }

        private void ChatForm_Resize(object sender, EventArgs e)
        {
            tbEntry.Width = this.Width - 116;
        }

        private void btSaveLog_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                tbDisplay.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

    }
}
