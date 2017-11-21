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
    public partial class AccessGroupEditor : Form
    {
        public AccessGroupEditor()
        {
            InitializeComponent();
        }

        public void SetCaption(int mode)
        {
            //0 = Add, 1 = Edit
            Text = (mode == 0) ? "Add Access Group" : "Edit Access Group";
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            bool formValid = ltbAccessGroupNumber.NumericValidation();
            if (formValid) formValid = ltbMinutesPerCall.NumericValidation();
            if (formValid) formValid = ltbCallsPerDay.NumericValidation();
            if (formValid) formValid = ltbDescription.BasicValidation();
            if (formValid) formValid = ltbTitle.BasicValidation();
            if (formValid) this.DialogResult = DialogResult.OK;
        }

        public void SetValues(AccessGroup accessGroup)
        {
            ltbAccessGroupNumber.EditText = accessGroup.AccessGroupNumber.ToString();
            ltbCallsPerDay.EditText = accessGroup.CallsPerDay.ToString();
            ltbDescription.EditText = accessGroup.Description;
            cbRemoteMaint.Checked = accessGroup.Flag_Remote_Maintenance;
            cbSysOp.Checked = accessGroup.Is_SysOp;
            ltbMinutesPerCall.EditText = accessGroup.MinutesPerCall.ToString();
            ltbTitle.EditText = accessGroup.Title;
        }

        public void ReturnValues(ref AccessGroup accessGroup)
        {
            accessGroup.AccessGroupNumber = int.Parse(ltbAccessGroupNumber.EditText);
            accessGroup.CallsPerDay = int.Parse(ltbCallsPerDay.EditText);
            accessGroup.Description = ltbDescription.EditText;
            accessGroup.Flag_Remote_Maintenance = cbRemoteMaint.Checked;
            accessGroup.Is_SysOp = cbSysOp.Checked;
            accessGroup.MinutesPerCall = int.Parse(ltbMinutesPerCall.EditText);
            accessGroup.Title = ltbTitle.EditText;
        }

    }
}
