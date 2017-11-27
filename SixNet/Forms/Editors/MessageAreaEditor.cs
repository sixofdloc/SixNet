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
    public partial class MessageAreaEditor : Form
    {
        public MessageAreaEditor()
        {
            InitializeComponent();
        }

        public void Initialize(int mode, DataInterface _dataInterface)
        {
            //0 = Add, 1 = Edit
            Text = (mode == 0) ? "Add Message Area" : "Edit Message Area";
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
            if (formValid) this.DialogResult = DialogResult.OK;

        }

        public void SetValues(MessageBaseArea messageBaseArea)
        {
            ltbTitle.EditText = messageBaseArea.Title;
            ltbDescription.EditText = messageBaseArea.LongDescription;
            lsParentArea.SelectedValue = messageBaseArea.ParentAreaId;
            lsMinimumAccessLevel.SelectedValue = messageBaseArea.AccessLevel;
        }

        public void ReturnValues(ref MessageBaseArea messageBaseArea)
        {
            messageBaseArea.Title = ltbTitle.EditText;
            messageBaseArea.LongDescription = ltbDescription.EditText;
            messageBaseArea.ParentAreaId = lsParentArea.SelectedValue;
            messageBaseArea.AccessLevel = lsMinimumAccessLevel.SelectedValue;
        }

    }
}
