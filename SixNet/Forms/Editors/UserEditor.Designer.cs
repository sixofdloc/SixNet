namespace SixNet_GUI.Forms.Editors
{
    partial class UserEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ltbComputerType = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbRealName = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.lsAccessLevel = new SixNet_GUI.User_Controls.LabelledSelector();
            this.ltbEmail = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbUsername = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbPassword = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbLastCallDetails = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.Location = new System.Drawing.Point(97, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(132, 30);
            this.btCancel.TabIndex = 8;
            this.btCancel.TabStop = false;
            this.btCancel.Text = "CANCEL";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btSave.Location = new System.Drawing.Point(229, 0);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(132, 30);
            this.btSave.TabIndex = 9;
            this.btSave.TabStop = false;
            this.btSave.Text = "SAVE";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 30);
            this.panel1.TabIndex = 0;
            // 
            // ltbComputerType
            // 
            this.ltbComputerType.AllowEmpty = false;
            this.ltbComputerType.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbComputerType.Editable = true;
            this.ltbComputerType.EditText = "";
            this.ltbComputerType.EditWidth = 355;
            this.ltbComputerType.IsPassword = false;
            this.ltbComputerType.LabelText = "Computer Type";
            this.ltbComputerType.Location = new System.Drawing.Point(0, 184);
            this.ltbComputerType.Max = 0;
            this.ltbComputerType.MaxLength = 255;
            this.ltbComputerType.Min = 0;
            this.ltbComputerType.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbComputerType.Name = "ltbComputerType";
            this.ltbComputerType.NumbersOnly = false;
            this.ltbComputerType.Padding = new System.Windows.Forms.Padding(3);
            this.ltbComputerType.Size = new System.Drawing.Size(361, 46);
            this.ltbComputerType.TabIndex = 2;
            // 
            // ltbRealName
            // 
            this.ltbRealName.AllowEmpty = false;
            this.ltbRealName.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbRealName.Editable = true;
            this.ltbRealName.EditText = "";
            this.ltbRealName.EditWidth = 355;
            this.ltbRealName.IsPassword = false;
            this.ltbRealName.LabelText = "Real Name";
            this.ltbRealName.Location = new System.Drawing.Point(0, 138);
            this.ltbRealName.Max = 0;
            this.ltbRealName.MaxLength = 255;
            this.ltbRealName.Min = 0;
            this.ltbRealName.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbRealName.Name = "ltbRealName";
            this.ltbRealName.NumbersOnly = false;
            this.ltbRealName.Padding = new System.Windows.Forms.Padding(3);
            this.ltbRealName.Size = new System.Drawing.Size(361, 46);
            this.ltbRealName.TabIndex = 1;
            // 
            // lsAccessLevel
            // 
            this.lsAccessLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsAccessLevel.LabelText = "Access Level";
            this.lsAccessLevel.Location = new System.Drawing.Point(0, 230);
            this.lsAccessLevel.Name = "lsAccessLevel";
            this.lsAccessLevel.NoneOption = false;
            this.lsAccessLevel.Padding = new System.Windows.Forms.Padding(8);
            this.lsAccessLevel.SelectedItem = null;
            this.lsAccessLevel.SelectedValue = -1;
            this.lsAccessLevel.Size = new System.Drawing.Size(361, 56);
            this.lsAccessLevel.TabIndex = 3;
            // 
            // ltbEmail
            // 
            this.ltbEmail.AllowEmpty = false;
            this.ltbEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbEmail.Editable = true;
            this.ltbEmail.EditText = "";
            this.ltbEmail.EditWidth = 355;
            this.ltbEmail.IsPassword = false;
            this.ltbEmail.LabelText = "Email";
            this.ltbEmail.Location = new System.Drawing.Point(0, 46);
            this.ltbEmail.Max = 0;
            this.ltbEmail.MaxLength = 255;
            this.ltbEmail.Min = 0;
            this.ltbEmail.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbEmail.Name = "ltbEmail";
            this.ltbEmail.NumbersOnly = false;
            this.ltbEmail.Padding = new System.Windows.Forms.Padding(3);
            this.ltbEmail.Size = new System.Drawing.Size(361, 46);
            this.ltbEmail.TabIndex = 4;
            // 
            // ltbUsername
            // 
            this.ltbUsername.AllowEmpty = false;
            this.ltbUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbUsername.Editable = true;
            this.ltbUsername.EditText = "";
            this.ltbUsername.EditWidth = 355;
            this.ltbUsername.IsPassword = false;
            this.ltbUsername.LabelText = "Username";
            this.ltbUsername.Location = new System.Drawing.Point(0, 0);
            this.ltbUsername.Max = 0;
            this.ltbUsername.MaxLength = 255;
            this.ltbUsername.Min = 0;
            this.ltbUsername.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbUsername.Name = "ltbUsername";
            this.ltbUsername.NumbersOnly = false;
            this.ltbUsername.Padding = new System.Windows.Forms.Padding(3);
            this.ltbUsername.Size = new System.Drawing.Size(361, 46);
            this.ltbUsername.TabIndex = 5;
            // 
            // ltbPassword
            // 
            this.ltbPassword.AllowEmpty = false;
            this.ltbPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbPassword.Editable = true;
            this.ltbPassword.EditText = "";
            this.ltbPassword.EditWidth = 355;
            this.ltbPassword.IsPassword = true;
            this.ltbPassword.LabelText = "Password";
            this.ltbPassword.Location = new System.Drawing.Point(0, 92);
            this.ltbPassword.Max = 0;
            this.ltbPassword.MaxLength = 255;
            this.ltbPassword.Min = 0;
            this.ltbPassword.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbPassword.Name = "ltbPassword";
            this.ltbPassword.NumbersOnly = false;
            this.ltbPassword.Padding = new System.Windows.Forms.Padding(3);
            this.ltbPassword.Size = new System.Drawing.Size(361, 46);
            this.ltbPassword.TabIndex = 6;
            // 
            // ltbLastCallDetails
            // 
            this.ltbLastCallDetails.AllowEmpty = false;
            this.ltbLastCallDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbLastCallDetails.Editable = false;
            this.ltbLastCallDetails.EditText = "Never Called";
            this.ltbLastCallDetails.EditWidth = 355;
            this.ltbLastCallDetails.IsPassword = false;
            this.ltbLastCallDetails.LabelText = "Last Call Details";
            this.ltbLastCallDetails.Location = new System.Drawing.Point(0, 286);
            this.ltbLastCallDetails.Max = 0;
            this.ltbLastCallDetails.MaxLength = 255;
            this.ltbLastCallDetails.Min = 0;
            this.ltbLastCallDetails.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbLastCallDetails.Name = "ltbLastCallDetails";
            this.ltbLastCallDetails.NumbersOnly = false;
            this.ltbLastCallDetails.Padding = new System.Windows.Forms.Padding(3);
            this.ltbLastCallDetails.Size = new System.Drawing.Size(361, 46);
            this.ltbLastCallDetails.TabIndex = 7;
            // 
            // UserEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 435);
            this.ControlBox = false;
            this.Controls.Add(this.ltbLastCallDetails);
            this.Controls.Add(this.lsAccessLevel);
            this.Controls.Add(this.ltbComputerType);
            this.Controls.Add(this.ltbRealName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ltbPassword);
            this.Controls.Add(this.ltbEmail);
            this.Controls.Add(this.ltbUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UserEditor";
            this.Text = "User Editor";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel panel1;
        private User_Controls.LabelledTextbox ltbRealName;
        private User_Controls.LabelledTextbox ltbComputerType;
        private User_Controls.LabelledSelector lsAccessLevel;
        private User_Controls.LabelledTextbox ltbEmail;
        private User_Controls.LabelledTextbox ltbUsername;
        private User_Controls.LabelledTextbox ltbPassword;
        private User_Controls.LabelledTextbox ltbLastCallDetails;
    }
}