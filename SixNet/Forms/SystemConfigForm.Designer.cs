namespace SixNet_GUI.Forms
{
    partial class SystemConfigForm
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
            this.btSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBBSName = new System.Windows.Forms.TextBox();
            this.tbBBSURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBBSPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSysOpHandle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSysOpEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.cbSysOpAccount = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(140, 248);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 0;
            this.btSave.Text = "SAVE";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "BBS Name";
            // 
            // tbBBSName
            // 
            this.tbBBSName.Location = new System.Drawing.Point(12, 30);
            this.tbBBSName.Name = "tbBBSName";
            this.tbBBSName.Size = new System.Drawing.Size(196, 20);
            this.tbBBSName.TabIndex = 2;
            // 
            // tbBBSURL
            // 
            this.tbBBSURL.Location = new System.Drawing.Point(12, 73);
            this.tbBBSURL.Name = "tbBBSURL";
            this.tbBBSURL.Size = new System.Drawing.Size(196, 20);
            this.tbBBSURL.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "BBS URL";
            // 
            // tbBBSPort
            // 
            this.tbBBSPort.Location = new System.Drawing.Point(225, 73);
            this.tbBBSPort.Name = "tbBBSPort";
            this.tbBBSPort.Size = new System.Drawing.Size(71, 20);
            this.tbBBSPort.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "BBS Port";
            // 
            // tbSysOpHandle
            // 
            this.tbSysOpHandle.Location = new System.Drawing.Point(12, 117);
            this.tbSysOpHandle.Name = "tbSysOpHandle";
            this.tbSysOpHandle.Size = new System.Drawing.Size(196, 20);
            this.tbSysOpHandle.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "SysOp Handle";
            // 
            // tbSysOpEmail
            // 
            this.tbSysOpEmail.Location = new System.Drawing.Point(12, 161);
            this.tbSysOpEmail.Name = "tbSysOpEmail";
            this.tbSysOpEmail.Size = new System.Drawing.Size(196, 20);
            this.tbSysOpEmail.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "SysOp Email";
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(221, 248);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = "CANCEL";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // cbSysOpAccount
            // 
            this.cbSysOpAccount.FormattingEnabled = true;
            this.cbSysOpAccount.Location = new System.Drawing.Point(12, 211);
            this.cbSysOpAccount.Name = "cbSysOpAccount";
            this.cbSysOpAccount.Size = new System.Drawing.Size(284, 21);
            this.cbSysOpAccount.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "SysOp User Account";
            // 
            // SystemConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 285);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbSysOpAccount);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tbSysOpEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSysOpHandle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbBBSPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBBSURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbBBSName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SystemConfigForm";
            this.Text = "System Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbBBSName;
        private System.Windows.Forms.TextBox tbBBSURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBBSPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSysOpHandle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSysOpEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ComboBox cbSysOpAccount;
        private System.Windows.Forms.Label label6;
    }
}