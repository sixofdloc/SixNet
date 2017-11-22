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
    public partial class BBSConfigEditor : Form
    {
        public BBSConfigEditor()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            bool formValid = ltbBBSName.BasicValidation();
            if (formValid) formValid = ltbBBSPort.NumericValidation();
            if (formValid) formValid = ltbBBSUrl.BasicValidation();
            if (formValid) formValid = ltbSysOpEmail.BasicValidation();
            if (formValid) formValid = ltbSysOpHandle.BasicValidation();
            if (formValid) formValid = ltbSysOpMenuPassword.BasicValidation();
            if (formValid) this.DialogResult = DialogResult.OK;
        }

        public void SetValues(BBSConfig bbsConfig)
        {
            ltbBBSName.EditText = bbsConfig.BBS_Name;
            ltbBBSPort.EditText = bbsConfig.BBS_Port.ToString();
            ltbBBSUrl.EditText = bbsConfig.BBS_URL;
            ltbSysOpEmail.EditText = bbsConfig.SysOp_Email;
            ltbSysOpHandle.EditText = bbsConfig.SysOp_Handle;
            ltbSysOpMenuPassword.EditText = bbsConfig.SysopMenuPass;
        }

        public void ReturnValues(ref BBSConfig bbsConfig)
        {
            bbsConfig.BBS_Name = ltbBBSName.EditText;
            bbsConfig.BBS_Port = int.Parse(ltbBBSPort.EditText);
            bbsConfig.BBS_URL = ltbBBSUrl.EditText;
            bbsConfig.SysOp_Email = ltbSysOpEmail.EditText;
            bbsConfig.SysOp_Handle = ltbSysOpHandle.EditText;
            bbsConfig.SysopMenuPass = ltbSysOpMenuPassword.EditText;

        }

        private void ltbBBSPort_Edit_Finished(object sender, EventArgs e)
        {

        }
    }
}
