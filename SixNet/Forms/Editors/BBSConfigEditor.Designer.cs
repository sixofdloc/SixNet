﻿namespace SixNet_GUI.Forms.Editors
{
    partial class BBSConfigEditor
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
            this.ltbBBSName = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbBBSUrl = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbBBSPort = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbSysOpHandle = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbSysOpEmail = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbSysOpMenuPassword = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.Location = new System.Drawing.Point(119, 0);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(132, 42);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "CANCEL";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btSave
            // 
            this.btSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btSave.Location = new System.Drawing.Point(251, 0);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(132, 42);
            this.btSave.TabIndex = 8;
            this.btSave.Text = "SAVE";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 42);
            this.panel1.TabIndex = 2;
            // 
            // ltbBBSName
            // 
            this.ltbBBSName.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbBBSName.EditText = "";
            this.ltbBBSName.LabelText = "BBS Name";
            this.ltbBBSName.Location = new System.Drawing.Point(0, 0);
            this.ltbBBSName.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbBBSName.Name = "ltbBBSName";
            this.ltbBBSName.Padding = new System.Windows.Forms.Padding(3);
            this.ltbBBSName.Size = new System.Drawing.Size(383, 46);
            this.ltbBBSName.TabIndex = 1;
            // 
            // ltbBBSUrl
            // 
            this.ltbBBSUrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbBBSUrl.EditText = "";
            this.ltbBBSUrl.LabelText = "BBS URL";
            this.ltbBBSUrl.Location = new System.Drawing.Point(0, 46);
            this.ltbBBSUrl.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbBBSUrl.Name = "ltbBBSUrl";
            this.ltbBBSUrl.Padding = new System.Windows.Forms.Padding(3);
            this.ltbBBSUrl.Size = new System.Drawing.Size(383, 46);
            this.ltbBBSUrl.TabIndex = 2;
            // 
            // ltbBBSPort
            // 
            this.ltbBBSPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbBBSPort.EditText = "";
            this.ltbBBSPort.LabelText = "BBS Port";
            this.ltbBBSPort.Location = new System.Drawing.Point(0, 92);
            this.ltbBBSPort.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbBBSPort.Name = "ltbBBSPort";
            this.ltbBBSPort.Padding = new System.Windows.Forms.Padding(3);
            this.ltbBBSPort.Size = new System.Drawing.Size(383, 46);
            this.ltbBBSPort.TabIndex = 3;
            this.ltbBBSPort.Edit_Finished += new System.EventHandler(this.ltbBBSPort_Edit_Finished);
            // 
            // ltbSysOpHandle
            // 
            this.ltbSysOpHandle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbSysOpHandle.EditText = "";
            this.ltbSysOpHandle.LabelText = "SysOp Handle";
            this.ltbSysOpHandle.Location = new System.Drawing.Point(0, 138);
            this.ltbSysOpHandle.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbSysOpHandle.Name = "ltbSysOpHandle";
            this.ltbSysOpHandle.Padding = new System.Windows.Forms.Padding(3);
            this.ltbSysOpHandle.Size = new System.Drawing.Size(383, 46);
            this.ltbSysOpHandle.TabIndex = 4;
            // 
            // ltbSysOpEmail
            // 
            this.ltbSysOpEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbSysOpEmail.EditText = "";
            this.ltbSysOpEmail.LabelText = "SysOp Email";
            this.ltbSysOpEmail.Location = new System.Drawing.Point(0, 184);
            this.ltbSysOpEmail.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbSysOpEmail.Name = "ltbSysOpEmail";
            this.ltbSysOpEmail.Padding = new System.Windows.Forms.Padding(3);
            this.ltbSysOpEmail.Size = new System.Drawing.Size(383, 46);
            this.ltbSysOpEmail.TabIndex = 5;
            // 
            // ltbSysOpMenuPassword
            // 
            this.ltbSysOpMenuPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbSysOpMenuPassword.EditText = "";
            this.ltbSysOpMenuPassword.LabelText = "SysOp Menu Password";
            this.ltbSysOpMenuPassword.Location = new System.Drawing.Point(0, 230);
            this.ltbSysOpMenuPassword.MinimumSize = new System.Drawing.Size(200, 46);
            this.ltbSysOpMenuPassword.Name = "ltbSysOpMenuPassword";
            this.ltbSysOpMenuPassword.Padding = new System.Windows.Forms.Padding(3);
            this.ltbSysOpMenuPassword.Size = new System.Drawing.Size(383, 46);
            this.ltbSysOpMenuPassword.TabIndex = 6;
            // 
            // BBSConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 349);
            this.ControlBox = false;
            this.Controls.Add(this.ltbSysOpMenuPassword);
            this.Controls.Add(this.ltbSysOpEmail);
            this.Controls.Add(this.ltbSysOpHandle);
            this.Controls.Add(this.ltbBBSPort);
            this.Controls.Add(this.ltbBBSUrl);
            this.Controls.Add(this.ltbBBSName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BBSConfigEditor";
            this.Text = "Edit BBS Configuration";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel panel1;
        public User_Controls.LabelledTextbox ltbBBSName;
        public User_Controls.LabelledTextbox ltbBBSUrl;
        public User_Controls.LabelledTextbox ltbBBSPort;
        public User_Controls.LabelledTextbox ltbSysOpHandle;
        public User_Controls.LabelledTextbox ltbSysOpEmail;
        public User_Controls.LabelledTextbox ltbSysOpMenuPassword;
    }
}