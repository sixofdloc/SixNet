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
using System.IO;
using SixNet_GUI.Forms.Editors;

namespace SixNet
{
    public partial class MainForm : Form,IBBSHost
    {

        public const int MODE_ADD = 0;
        public const int MODE_EDIT = 1;

        private readonly FormUtils _formUtils;
        private readonly DataInterface _dataInterface;
        private readonly string _dbConfigStr;
        private BBSConfig _bbsConfig;

        #region Public Interface

        public Server testserv;
        public List<BBS> bbs_instances;
        
        delegate void GenericStateObjectCallback(StateObject so);
        delegate void GenericCallback();


        public MainForm()
        {
            InitializeComponent();
            _dbConfigStr = File.ReadAllText("BBSConfig.txt").Split('|')[1];
            _dataInterface = new DataInterface(_dbConfigStr);
            _formUtils = new FormUtils(_dataInterface);
            _bbsConfig = _dataInterface.GetBBSConfig();
            bbs_instances = new List<BBS>();
        }

        private void StartBBS(StateObject so)
        {
            BBS bbs =  new BBS(this, so,_dbConfigStr);
            if (bbs_instances == null) bbs_instances = new List<BBS>();
            bbs_instances.Add(bbs);
            Thread thread = new Thread(()=>bbs.Go());
            thread.Start();
            RefreshGrid();

        }

        public void AddConnection(StateObject so)
        {
            try
            {
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            testserv = new Server(_bbsConfig.BBS_Port, "Client Session", "", AddConnection, DropConnection);
            SetServerStatusLabel("RUNNING");
            this.Text = "DLoCBBS Control GUI V" + typeof(MainForm).Assembly.GetName().Version;
            testserv.Start();
            GridUpdateTimer.Enabled = true;
            SetServerStatusLabel("RUNNING");
            _formUtils.RefreshUsers(dg_Users);
            _formUtils.RefreshAccessGroups(dg_AccessGroups);
            _formUtils.RefreshCallLog(dgCallLog);
            _formUtils.RefreshMessageAreas(dgMessageBaseAreas);
            _formUtils.RefreshMessageBases(dgMessageBases);
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

                try
                {
                    dataGridView1.Rows.Clear();
                    int i = 0;
                    foreach (BBS bbs in bbs_instances)
                    {
                        if (bbs.CurrentUser != null)
                        {
                            dataGridView1.Rows.Add(i, bbs.State_Object.GetHashCode(), bbs.State_Object.workSocket.RemoteEndPoint.ToString(), bbs.CurrentUser.Username,bbs.CurrentArea,(DateTime.Now.Subtract(bbs.ConnectionTimeStamp).ToString()));
                        }
                        else
                        {
                            dataGridView1.Rows.Add(i, bbs.State_Object.GetHashCode(), bbs.State_Object.workSocket.RemoteEndPoint.ToString(), "Pending Login", "Login", (DateTime.Now.Subtract(bbs.ConnectionTimeStamp).ToString()));
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
            RefreshGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _formUtils.RefreshAccessGroups(dg_AccessGroups);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            _formUtils.RefreshUsers(dg_Users);
        }

        private void dg_Users_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           EditUser(int.Parse(dg_Users.SelectedRows[0].Cells[0].Value.ToString()));
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BBSConfigEditor bbsConfigEditor = new BBSConfigEditor();
            bbsConfigEditor.SetValues(_bbsConfig);
            
            if (bbsConfigEditor.ShowDialog() == DialogResult.OK)
            {
                bbsConfigEditor.ReturnValues(ref _bbsConfig);
                _dataInterface.SaveBBSConfig(_bbsConfig);

            }
        }

        private void btAddAccessGroup_Click(object sender, EventArgs e)
        {
            var accessGroupEditor = new AccessGroupEditor();
            accessGroupEditor.SetCaption(MODE_ADD);
            if (accessGroupEditor.ShowDialog() == DialogResult.OK)
            {
                var accessGroup = new AccessGroup();
                accessGroupEditor.ReturnValues(ref accessGroup);
                _dataInterface.CreateAccessGroup(accessGroup);
                _formUtils.RefreshAccessGroups(dg_AccessGroups);
            }

        }

        private void btEditAccessGroup_Click(object sender, EventArgs e)
        {
            int AccessGroupId = (int)(dg_AccessGroups.SelectedRows[0].Cells[0].Value);
            var accessGroup = _dataInterface.GetAccessGroupById(AccessGroupId);
            if (accessGroup != null)
            {
                var accessGroupEditor = new AccessGroupEditor();
                accessGroupEditor.SetCaption(MODE_EDIT);
                accessGroupEditor.SetValues(accessGroup);
                if (accessGroupEditor.ShowDialog() == DialogResult.OK)
                {
                    accessGroupEditor.ReturnValues(ref accessGroup);
                    _dataInterface.UpdateAccessGroup(accessGroup);
                    _formUtils.RefreshAccessGroups(dg_AccessGroups);
                }

            }
        }


        private void btEditMessageArea_Click(object sender, EventArgs e)
        {
            int messageBaseAreaId = (int)(dgMessageBaseAreas.SelectedRows[0].Cells[0].Value);
            var messageBaseArea = _dataInterface.GetMessageBaseAreaById(messageBaseAreaId);
            if (messageBaseArea != null)
            {
                var messageAreaEditor = new MessageAreaEditor();
                messageAreaEditor.Initialize(MODE_EDIT, _dataInterface);
                messageAreaEditor.SetValues(messageBaseArea);
                if (messageAreaEditor.ShowDialog() == DialogResult.OK)
                {
                    messageAreaEditor.ReturnValues(ref messageBaseArea);
                    _dataInterface.UpdateMessageBaseArea(messageBaseArea);
                    _formUtils.RefreshMessageAreas(dgMessageBaseAreas);
                }

            }
        }

        private void btAddMessageArea_Click(object sender, EventArgs e)
        {
            var messageAreaEditor = new MessageAreaEditor();
            messageAreaEditor.Initialize(MODE_ADD, _dataInterface);
            if (messageAreaEditor.ShowDialog()==DialogResult.OK)
            {
                var messageBaseArea = new MessageBaseArea();
                messageAreaEditor.ReturnValues(ref messageBaseArea);
                _dataInterface.CreateMessageBaseArea(messageBaseArea);
                _formUtils.RefreshMessageAreas(dgMessageBaseAreas);
            }
        }

        private void btAddMessageBase_Click(object sender, EventArgs e)
        {
            var messageBaseEditor = new MessageBaseEditor();
            messageBaseEditor.Initialize(MODE_ADD, _dataInterface);
            if (messageBaseEditor.ShowDialog() == DialogResult.OK)
            {
                var messageBase = new MessageBase();
                messageBaseEditor.ReturnValues(ref messageBase);
                _dataInterface.CreateMessageBase(messageBase);
                _formUtils.RefreshMessageAreas(dgMessageBases);
            }
        }

        private void btEditMessageBase_Click(object sender, EventArgs e)
        {
            int messageBaseId = (int)(dgMessageBases.SelectedRows[0].Cells[0].Value);
            var messageBase = _dataInterface.GetMessageBaseById(messageBaseId);
            if (messageBase != null)
            {
                var messageBaseEditor = new MessageBaseEditor();
                messageBaseEditor.Initialize(MODE_EDIT, _dataInterface);
                messageBaseEditor.SetValues(messageBase);
                if (messageBaseEditor.ShowDialog() == DialogResult.OK)
                {
                    messageBaseEditor.ReturnValues(ref messageBase);
                    _dataInterface.UpdateMessageBase(messageBase);
                    _formUtils.RefreshMessageBases(dgMessageBases);
                }

            }
        }

        private void btAddUser_Click(object sender, EventArgs e)
        {
            var userEditor = new UserEditor();
            userEditor.Initialize(MODE_ADD, _dataInterface);
            if (userEditor.ShowDialog() == DialogResult.OK)
            {
                var user = new User();
                userEditor.ReturnValues(ref user);
                _dataInterface.CreateUser(user);
                _formUtils.RefreshUsers(dg_Users);
            }
        }

        private void btEditUser_Click(object sender, EventArgs e)
        {
            var userId = (int)(dg_Users.SelectedRows[0].Cells[0].Value);
            EditUser(userId);
        }

        private void EditUser(int userId)
        {
            var user = _dataInterface.GetUserById(userId);
            if (user != null)
            {
                var userEditor = new UserEditor();
                userEditor.Initialize(MODE_EDIT, _dataInterface);
                if (userEditor.ShowDialog() == DialogResult.OK)
                {
                    userEditor.ReturnValues(ref user);
                    _dataInterface.UpdateUser(user);
                    _formUtils.RefreshUsers(dg_Users);
                }
            }

        }
    }
}
