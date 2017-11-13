using System;
using System.Windows.Forms;
using SixNet_BBS_Data;

namespace SixNet_GUI.Forms
{
    public partial class UserEdit : Form
    {
        public User user;

        private readonly DataInterface _dataInterface;

        public UserEdit(DataInterface dataInterface)
        {
            InitializeComponent();
            _dataInterface = dataInterface;
        }

        private void btResetPass_Click(object sender, EventArgs e)
        {

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void UserEdit_Load(object sender, EventArgs e)
        {
            tbComputer.DataBindings.Add("Text", user, "ComputerType");
            tbUsername.DataBindings.Add("Text", user, "Username");
            tbEmail.DataBindings.Add("Text", user, "Email");
            //cbAccess.DataBindings.Add("SelectedIndex", user, "AccessLevel");
            cbAccess.DataSource = _dataInterface.AccessGroups();
            cbAccess.ValueMember = "AccessGroupID";
            cbAccess.DisplayMember = "Title";
        }

  
    }
}
