using SixNet_BBS_Data;
using SixNet_GUI.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SixNet_GUI.Forms.Editors
{
    public partial class UserEditor : Form
    {
        public UserEditor()
        {
            InitializeComponent();
        }

        public void Initialize(int mode, DataInterface _dataInterface)
        {
            //0 = Add, 1 = Edit
            Text = (mode == 0) ? "Add User" : "Edit User";
            lsAccessLevel.Populate(
                _dataInterface.AccessGroups().Select(
                    p => new Classes.ComboBoxListItem(p.AccessGroupId, p.Description + " (" + p.AccessGroupNumber + ")")
                    ).ToList()
            );
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            bool formValid = ltbUsername.BasicValidation();
            if (formValid) formValid = ltbPassword.BasicValidation();
            if (formValid) formValid = lsAccessLevel.BasicValidation();
            if (formValid) formValid = ltbEmail.BasicValidation();
            if (formValid) this.DialogResult = DialogResult.OK;
        }

        public void SetValues(User user)
        {
            lsAccessLevel.SelectedValue = user.AccessLevel;
            ltbComputerType.EditText = user.ComputerType;
            ltbEmail.EditText = user.Email;
            ltbPassword.EditText = user.HashedPassword;
            ltbRealName.EditText = user.RealName;
            ltbUsername.EditText = user.Username;
            ltbLastCallDetails.EditText = user.LastConnectionIP == null?"Never Called" : (user.LastConnection + " from " + user.LastConnectionIP);
        }

        public void ReturnValues(ref User user)
        {
            user.AccessLevel = lsAccessLevel.SelectedValue;
            user.ComputerType = ltbComputerType.EditText;
            user.Email = ltbEmail.EditText;
            user.HashedPassword = ltbPassword.EditText;
            user.RealName = ltbRealName.EditText;
            user.Username = ltbUsername.EditText;
        }

    }
}
