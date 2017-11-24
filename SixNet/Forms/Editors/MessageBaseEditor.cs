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
    public partial class MessageBaseEditor : Form
    {
        public MessageBaseEditor()
        {
            InitializeComponent();
        }

        public void Initialize(int mode, DataInterface _dataInterface)
        {
            //0 = Add, 1 = Edit
            Text = (mode == 0) ? "Add Message Base" : "Edit Message Base";
            lsParentArea.Populate(
                _dataInterface.MessageBaseAreas().Select(
                    p => new Classes.ComboBoxListItem(p.MessageBaseAreaId, p.LongDescription + " (" + p.MessageBaseAreaId + ")")
                    ).ToList()
            );
            lsMinimumAccessLevel.Populate(
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
            bool formValid = ltbTitle.BasicValidation();
            if (formValid) formValid = ltbDescription.BasicValidation();
            if (formValid) formValid = lsMinimumAccessLevel.BasicValidation();
            if (formValid) formValid = lsParentArea.BasicValidation();
        }

        public void SetValues(MessageBase messageBase)
        {
            ltbTitle.EditText = messageBase.Title;
            ltbDescription.EditText = messageBase.LongDescription;
            lsParentArea.SelectedValue = messageBase.ParentArea;
            lsMinimumAccessLevel.SelectedValue = messageBase.AccessLevel;
        }

        public void ReturnValues(ref MessageBase messageBase)
        {
            messageBase.Title = ltbTitle.EditText;
            messageBase.LongDescription = ltbDescription.EditText;
            messageBase.ParentArea = lsParentArea.SelectedValue;
            messageBase.AccessLevel = lsMinimumAccessLevel.SelectedValue;
        }

    }
}
