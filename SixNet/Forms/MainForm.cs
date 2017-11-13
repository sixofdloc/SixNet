using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SixNet.Forms;
using SixNet_BBS;
using System.Threading;
using SixNet_GUI.Properties;
using SixNet_GUI.Forms;
using SixNet_GUI;
using SixNet_Comm;
using SixNet_BBS.Interfaces;
using SixNet_Logger;
using SixNet_BBS_Data;

namespace SixNet
{
    public partial class MainForm : Form,IBBSHost
    {

        private readonly FormUtils _formUtils;
        private readonly DataInterface _dataInterface;

        #region Public Interface

        public Server testserv;
        public List<BBS> bbs_instances;
        
        //public List<BBS> BBS_Ports { get; set; }

        delegate void GenericStateObjectCallback(StateObject so);
        delegate void GenericCallback();

        private void StartBBS(StateObject so)
        {
            BBS bbs =  new BBS(this, so,Settings.Default.BBSDataContext);
            if (bbs_instances == null) bbs_instances = new List<BBS>();
            bbs_instances.Add(bbs);
          //  BBS_Ports.Add(bbs);
            Thread thread = new Thread(()=>bbs.Go());
            thread.Start();
            RefreshGrid();

        }

        public void AddConnection(StateObject so)
        {
            try
            {
                    //Thread thread = new Thread(()=>StartBBS(so));
                    //thread.Start();
                   // RefreshGrid();
                StartBBS(so);
            }
            catch (Exception e)
            {
                LoggingAPI.LogEntry("Exception in MainForm.AddConnection: " + e.Message);
            }
        }

        public void DropConnection(StateObject so)
        {
            BBS bbs = bbs_instances.FirstOrDefault(p=>p.State_Object.Equals(so));
            if (bbs != null)
            {
                bbs_instances.Remove(bbs);
            }
                RefreshGrid();
        }

        #endregion







        public MainForm()
        {
            InitializeComponent();
            _dataInterface = new DataInterface(Settings.Default.BBSDataContext);
            _formUtils = new FormUtils(_dataInterface);
           // BBS_Ports = new List<BBS>();
            bbs_instances = new List<BBS>();
            //SixNet_BBS_Data.DataInterface.Initialize(Settings.Default.BBSDataContext);

        }






        private void Form1_Load(object sender, EventArgs e)
        {
            testserv = new Server(Settings.Default.Port, "Client Session", "", AddConnection, DropConnection);
            SetServerStatusLabel("RUNNING");
            this.Text = "DLoCBBS Control GUI V" + typeof(MainForm).Assembly.GetName().Version;
            //dataGridView1.DataSource = testserv.connectedSocks;
            testserv.Start();
            //MUD.Initialize();
            //btStartServer.Enabled = false;
            //btStopServer.Enabled = true;
            GridUpdateTimer.Enabled = true;
            SetServerStatusLabel("RUNNING");
            _formUtils.RefreshUsers(dg_Users);
            _formUtils.RefreshAccessGroups(dg_AccessGroups);
            _formUtils.RefreshCallLog(dgCallLog);
            _formUtils.RefreshMessageAreas(dgMessageBaseAreas);
            _formUtils.RefreshMessageBases(dgMessageBases);


        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void RefreshGrid()
        {
            if (this.dataGridView1.InvokeRequired)
            {
                GenericCallback d = new GenericCallback(RefreshGrid);
                this.Invoke(d);
            }
            else
            {
                //BBS bbs = BBS_Ports.FirstOrDefault(p => p.State_Object.Equals(so));
                //if (bbs !=null) BBS_Ports.Remove(bbs);
                //for (int i = 1; i < dataGridView1.Rows.Count; i++)
                //{
                //    if ((int)dataGridView1.Rows[i].Cells[1].Value == so.GetHashCode())
                //    {
                //        dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                //    }
                //}

                try
                {
                    //                dataGridView1.BindingContext.
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    foreach (BBS bbs in bbs_instances)
                    {
                        if (bbs.CurrentUser != null)
                        {
                            dataGridView1.Rows.Add(i, bbs.State_Object.GetHashCode(), bbs.CurrentUser.Username,bbs.CurrentArea,(DateTime.Now.Subtract(bbs.ConnectionTimeStamp).ToString()));
                        }
                        else
                        {
                            dataGridView1.Rows.Add(i, bbs.State_Object.GetHashCode(), "Pending Login", "Login", (DateTime.Now.Subtract(bbs.ConnectionTimeStamp).ToString()));
                        }
                        i++;
                    }
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    LoggingAPI.LogEntry(ex.Message);
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void messageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MessagePrompt mp = new MessagePrompt();
                if (mp.ShowDialog() == DialogResult.OK)
                {
                    //which row is highlighted
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = int.Parse(row.Cells[1].Value.ToString());
                        StateObject so = testserv.connectedSocks.FirstOrDefault(p => p.GetHashCode().Equals(id));
                        if (so != null)
                            testserv.Send(so.workSocket, mp.textBox1.Text + "\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAPI.LogEntry(ex.Message);
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //which row is highlighted
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = int.Parse(row.Cells[1].Value.ToString());
                        StateObject so = testserv.connectedSocks.FirstOrDefault(p => p.GetHashCode().Equals(id));
                        if (so != null)
                            testserv.Disconnect(so);
                        BBS bbs = bbs_instances.FirstOrDefault(p => p.GetHashCode().Equals(id));
                        bbs_instances.Remove(bbs);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAPI.LogEntry(ex.Message);
            }
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = int.Parse(row.Cells[1].Value.ToString());
                    BBS bbs2 = bbs_instances.FirstOrDefault(p=>p.State_Object.GetHashCode().Equals(id));
                    if (bbs2 != null)
                    {
                        ChatForm of = new ChatForm(_dataInterface);
                        of.StartChat(bbs2,testserv);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingAPI.LogEntry(ex.Message);
            }

        }

        private void SetServerStatusLabel(string s)
        {
            toolStripStatusLabel1.Text = s;
        }

        private void GridUpdateTimer_Tick(object sender, EventArgs e)
        {
           // RefreshGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _formUtils.RefreshAccessGroups(dg_AccessGroups);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            _formUtils.RefreshUsers(dg_Users);
        }







        private void dg_Users_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_Users_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _formUtils.EditUser(int.Parse(dg_Users.SelectedRows[0].Cells[0].Value.ToString()));
        }

        public List<SixNet_BBS.BBS> GetAllNodes()
        {
            return bbs_instances;
        }

        private void btRefreshMessageAreas_Click(object sender, EventArgs e)
        {
            _formUtils.RefreshMessageAreas(dgMessageBaseAreas);
        }

        private void btRefreshMessageBases_Click(object sender, EventArgs e)
        {
            _formUtils.RefreshMessageBases(dgMessageBases);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _formUtils.RefreshCallLog(dgCallLog);
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            foreach (Object o in testserv.connectedSocks)
            {
                StateObject tempobj = (StateObject)o;
                testserv.Send(tempobj.workSocket, tempobj.id + ": System is shutting down.");
                tempobj.workSocket.Disconnect(false);
            }
            testserv.Stop();
            SetServerStatusLabel("STOPPED");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
