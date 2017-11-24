namespace SixNet_GUI.Forms.Editors
{
    partial class MessageAreaEditor
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
            this.ltbDescription = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.ltbTitle = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.lsMinimumAccessLevel = new SixNet_GUI.User_Controls.LabelledSelector();
            this.lsParentArea = new SixNet_GUI.User_Controls.LabelledSelector();
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
            this.panel1.Location = new System.Drawing.Point(0, 211);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 30);
            this.panel1.TabIndex = 0;
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
            this.ltbDescription.Size = new System.Drawing.Size(361, 46);
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
            this.ltbTitle.Size = new System.Drawing.Size(361, 46);
            this.ltbTitle.TabIndex = 1;
            // 
            // lsMinimumAccessLevel
            // 
            this.lsMinimumAccessLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsMinimumAccessLevel.LabelText = "Minimum Access Level";
            this.lsMinimumAccessLevel.Location = new System.Drawing.Point(0, 92);
            this.lsMinimumAccessLevel.Name = "lsMinimumAccessLevel";
            this.lsMinimumAccessLevel.NoneOption = false;
            this.lsMinimumAccessLevel.Padding = new System.Windows.Forms.Padding(8);
            this.lsMinimumAccessLevel.SelectedItem = null;
            this.lsMinimumAccessLevel.Size = new System.Drawing.Size(361, 56);
            this.lsMinimumAccessLevel.TabIndex = 3;
            // 
            // lsParentArea
            // 
            this.lsParentArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsParentArea.LabelText = "Parent Area";
            this.lsParentArea.Location = new System.Drawing.Point(0, 148);
            this.lsParentArea.Name = "lsParentArea";
            this.lsParentArea.NoneOption = true;
            this.lsParentArea.Padding = new System.Windows.Forms.Padding(8);
            this.lsParentArea.SelectedItem = null;
            this.lsParentArea.Size = new System.Drawing.Size(361, 56);
            this.lsParentArea.TabIndex = 4;
            // 
            // MessageAreaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 241);
            this.ControlBox = false;
            this.Controls.Add(this.lsParentArea);
            this.Controls.Add(this.lsMinimumAccessLevel);
            this.Controls.Add(this.ltbDescription);
            this.Controls.Add(this.ltbTitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MessageAreaEditor";
            this.Text = "Message Area Editor";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Panel panel1;
        private User_Controls.LabelledTextbox ltbTitle;
        private User_Controls.LabelledTextbox ltbDescription;
        private User_Controls.LabelledSelector lsMinimumAccessLevel;
        private User_Controls.LabelledSelector lsParentArea;
    }
}