namespace SixNet_GUI.Forms.Editors
{
    partial class AccessGroupEditor
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbRemoteMaint = new System.Windows.Forms.CheckBox();
            this.cbSysOp = new System.Windows.Forms.CheckBox();
            this.ltbCallsPerDay = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbMinutesPerCall = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbAccessGroupNumber = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbDescription = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbTitle = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btCancel.Location = new System.Drawing.Point(95, 0);
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
            this.btSave.Location = new System.Drawing.Point(227, 0);
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
            this.panel1.Location = new System.Drawing.Point(0, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 30);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ltbCallsPerDay);
            this.panel2.Controls.Add(this.ltbMinutesPerCall);
            this.panel2.Controls.Add(this.ltbAccessGroupNumber);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(359, 53);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbRemoteMaint);
            this.panel3.Controls.Add(this.cbSysOp);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 145);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(359, 36);
            this.panel3.TabIndex = 0;
            // 
            // cbRemoteMaint
            // 
            this.cbRemoteMaint.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbRemoteMaint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemoteMaint.Location = new System.Drawing.Point(178, 0);
            this.cbRemoteMaint.Name = "cbRemoteMaint";
            this.cbRemoteMaint.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cbRemoteMaint.Size = new System.Drawing.Size(178, 36);
            this.cbRemoteMaint.TabIndex = 7;
            this.cbRemoteMaint.Text = "Remote Maintenance";
            this.cbRemoteMaint.UseVisualStyleBackColor = true;
            // 
            // cbSysOp
            // 
            this.cbSysOp.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbSysOp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSysOp.Location = new System.Drawing.Point(0, 0);
            this.cbSysOp.Name = "cbSysOp";
            this.cbSysOp.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cbSysOp.Size = new System.Drawing.Size(178, 36);
            this.cbSysOp.TabIndex = 6;
            this.cbSysOp.Text = "Is SysOp";
            this.cbSysOp.UseVisualStyleBackColor = true;
            // 
            // ltbCallsPerDay
            // 
            this.ltbCallsPerDay.AllowEmpty = true;
            this.ltbCallsPerDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.ltbCallsPerDay.EditText = "";
            this.ltbCallsPerDay.EditWidth = 87;
            this.ltbCallsPerDay.LabelText = "Calls Per Day";
            this.ltbCallsPerDay.Location = new System.Drawing.Point(238, 0);
            this.ltbCallsPerDay.Max = 255;
            this.ltbCallsPerDay.MaxLength = 3;
            this.ltbCallsPerDay.Min = 0;
            this.ltbCallsPerDay.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbCallsPerDay.Name = "ltbCallsPerDay";
            this.ltbCallsPerDay.NumbersOnly = true;
            this.ltbCallsPerDay.Padding = new System.Windows.Forms.Padding(3);
            this.ltbCallsPerDay.Size = new System.Drawing.Size(119, 53);
            this.ltbCallsPerDay.TabIndex = 5;
            // 
            // ltbMinutesPerCall
            // 
            this.ltbMinutesPerCall.AllowEmpty = true;
            this.ltbMinutesPerCall.Dock = System.Windows.Forms.DockStyle.Left;
            this.ltbMinutesPerCall.EditText = "";
            this.ltbMinutesPerCall.EditWidth = 87;
            this.ltbMinutesPerCall.LabelText = "Minutes Per Call";
            this.ltbMinutesPerCall.Location = new System.Drawing.Point(119, 0);
            this.ltbMinutesPerCall.Max = 255;
            this.ltbMinutesPerCall.MaxLength = 3;
            this.ltbMinutesPerCall.Min = 0;
            this.ltbMinutesPerCall.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbMinutesPerCall.Name = "ltbMinutesPerCall";
            this.ltbMinutesPerCall.NumbersOnly = true;
            this.ltbMinutesPerCall.Padding = new System.Windows.Forms.Padding(3);
            this.ltbMinutesPerCall.Size = new System.Drawing.Size(119, 53);
            this.ltbMinutesPerCall.TabIndex = 4;
            // 
            // ltbAccessGroupNumber
            // 
            this.ltbAccessGroupNumber.AllowEmpty = true;
            this.ltbAccessGroupNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.ltbAccessGroupNumber.EditText = "";
            this.ltbAccessGroupNumber.EditWidth = 87;
            this.ltbAccessGroupNumber.LabelText = "Group #";
            this.ltbAccessGroupNumber.Location = new System.Drawing.Point(0, 0);
            this.ltbAccessGroupNumber.Max = 255;
            this.ltbAccessGroupNumber.MaxLength = 3;
            this.ltbAccessGroupNumber.Min = 0;
            this.ltbAccessGroupNumber.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbAccessGroupNumber.Name = "ltbAccessGroupNumber";
            this.ltbAccessGroupNumber.NumbersOnly = false;
            this.ltbAccessGroupNumber.Padding = new System.Windows.Forms.Padding(3);
            this.ltbAccessGroupNumber.Size = new System.Drawing.Size(119, 53);
            this.ltbAccessGroupNumber.TabIndex = 3;
            // 
            // ltbDescription
            // 
            this.ltbDescription.AllowEmpty = false;
            this.ltbDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbDescription.EditText = "";
            this.ltbDescription.EditWidth = 255;
            this.ltbDescription.LabelText = "Description";
            this.ltbDescription.Location = new System.Drawing.Point(0, 46);
            this.ltbDescription.Max = 0;
            this.ltbDescription.MaxLength = 255;
            this.ltbDescription.Min = 0;
            this.ltbDescription.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbDescription.Name = "ltbDescription";
            this.ltbDescription.NumbersOnly = false;
            this.ltbDescription.Padding = new System.Windows.Forms.Padding(3);
            this.ltbDescription.Size = new System.Drawing.Size(359, 46);
            this.ltbDescription.TabIndex = 2;
            // 
            // ltbTitle
            // 
            this.ltbTitle.AllowEmpty = false;
            this.ltbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltbTitle.EditText = "";
            this.ltbTitle.EditWidth = 255;
            this.ltbTitle.LabelText = "Title";
            this.ltbTitle.Location = new System.Drawing.Point(0, 0);
            this.ltbTitle.Max = 0;
            this.ltbTitle.MaxLength = 255;
            this.ltbTitle.Min = 0;
            this.ltbTitle.MinimumSize = new System.Drawing.Size(50, 46);
            this.ltbTitle.Name = "ltbTitle";
            this.ltbTitle.NumbersOnly = false;
            this.ltbTitle.Padding = new System.Windows.Forms.Padding(3);
            this.ltbTitle.Size = new System.Drawing.Size(359, 46);
            this.ltbTitle.TabIndex = 1;
            // 
            // AccessGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 229);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ltbDescription);
            this.Controls.Add(this.ltbTitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AccessGroupEditor";
            this.Text = "Access Group Editor";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel panel1;
        private User_Controls.LabelledTextbox ltbTitle;
        private User_Controls.LabelledTextbox ltbDescription;
        private System.Windows.Forms.Panel panel2;
        private User_Controls.LabelledTextbox ltbCallsPerDay;
        private User_Controls.LabelledTextbox ltbMinutesPerCall;
        private User_Controls.LabelledTextbox ltbAccessGroupNumber;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbRemoteMaint;
        private System.Windows.Forms.CheckBox cbSysOp;
    }
}