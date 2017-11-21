using SixNet_BBS_Data;
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
            this.DialogResult = DialogResult.OK;
        }

        public void SetValues(AccessGroup accessGroup)
        {
            //ltbBBSName.EditText = bbsConfig.BBS_Name;
            //ltbBBSPort.EditText = bbsConfig.BBS_Port.ToString();
            //ltbBBSUrl.EditText = bbsConfig.BBS_URL;
            //ltbSysOpEmail.EditText = bbsConfig.SysOp_Email;
            //ltbSysOpHandle.EditText = bbsConfig.SysOp_Handle;
            //ltbSysOpMenuPassword.EditText = bbsConfig.SysopMenuPass;
        }

        public void ReturnValues(ref AccessGroup accessGroup)
        {
            accessGroup.AccessGroupNumber = int.Parse(ltbAccessGroupNumber.Text);
            accessGroup.CallsPerDay = int.Parse(ltbCallsPerDay.Text);
           //bbsConfig.BBS_Name = ltbBBSName.EditText;
           //bbsConfig.BBS_Port  =     int.Parse(ltbBBSPort.EditText)           ;
           //bbsConfig.BBS_URL              =     ltbBBSUrl.EditText            ;
           //bbsConfig.SysOp_Email          =     ltbSysOpEmail.EditText        ;
           //bbsConfig.SysOp_Handle         =     ltbSysOpHandle.EditText       ;
           //bbsConfig.SysopMenuPass        =     ltbSysOpMenuPassword.EditText ;

        }

        //private void ltbBBSPort_Edit_Finished(object sender, EventArgs e)
        //{
        //    //Validate port
        //    int i = 0;
        //    if (Int32.TryParse(ltbBBSPort.EditText,out i))
        //    {
        //        if (i <= 0 || i >= 65536)
        //        {
        //            MessageBox.Show("BBS Port must be a number between 1 and 65535");
        //            ltbBBSPort.Focus();
        //        }
        //    } else {
        //        MessageBox.Show("BBS Port must be a number.");
        //        ltbBBSPort.Focus();

        //    }
        //}
    }
}
