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
            this.labelledTextbox1 = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.labelledTextbox2 = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelledTextbox3 = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.labelledTextbox4 = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.labelledTextbox5 = new SixNet_GUI.User_Controls.LabelledTextbox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(0, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 42);
            this.panel1.TabIndex = 2;
            // 
            // labelledTextbox1
            // 
            this.labelledTextbox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelledTextbox1.EditText = "";
            this.labelledTextbox1.EditWidth = 377;
            this.labelledTextbox1.LabelText = "Title";
            this.labelledTextbox1.Location = new System.Drawing.Point(0, 0);
            this.labelledTextbox1.MaxLength = 32767;
            this.labelledTextbox1.MinimumSize = new System.Drawing.Size(50, 46);
            this.labelledTextbox1.Name = "labelledTextbox1";
            this.labelledTextbox1.Padding = new System.Windows.Forms.Padding(3);
            this.labelledTextbox1.Size = new System.Drawing.Size(383, 46);
            this.labelledTextbox1.TabIndex = 4;
            // 
            // labelledTextbox2
            // 
            this.labelledTextbox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelledTextbox2.EditText = "";
            this.labelledTextbox2.EditWidth = 377;
            this.labelledTextbox2.LabelText = "Description";
            this.labelledTextbox2.Location = new System.Drawing.Point(0, 46);
            this.labelledTextbox2.MaxLength = 32767;
            this.labelledTextbox2.MinimumSize = new System.Drawing.Size(50, 46);
            this.labelledTextbox2.Name = "labelledTextbox2";
            this.labelledTextbox2.Padding = new System.Windows.Forms.Padding(3);
            this.labelledTextbox2.Size = new System.Drawing.Size(383, 46);
            this.labelledTextbox2.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelledTextbox5);
            this.panel2.Controls.Add(this.labelledTextbox4);
            this.panel2.Controls.Add(this.labelledTextbox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 53);
            this.panel2.TabIndex = 7;
            // 
            // labelledTextbox3
            // 
            this.labelledTextbox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelledTextbox3.EditText = "";
            this.labelledTextbox3.EditWidth = 100;
            this.labelledTextbox3.LabelText = "Group #";
            this.labelledTextbox3.Location = new System.Drawing.Point(0, 0);
            this.labelledTextbox3.MaxLength = 32767;
            this.labelledTextbox3.MinimumSize = new System.Drawing.Size(50, 46);
            this.labelledTextbox3.Name = "labelledTextbox3";
            this.labelledTextbox3.Padding = new System.Windows.Forms.Padding(3);
            this.labelledTextbox3.Size = new System.Drawing.Size(123, 53);
            this.labelledTextbox3.TabIndex = 4;
            // 
            // labelledTextbox4
            // 
            this.labelledTextbox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelledTextbox4.EditText = "";
            this.labelledTextbox4.EditWidth = 87;
            this.labelledTextbox4.LabelText = "Minutes Per Call";
            this.labelledTextbox4.Location = new System.Drawing.Point(123, 0);
            this.labelledTextbox4.MaxLength = 32767;
            this.labelledTextbox4.MinimumSize = new System.Drawing.Size(50, 46);
            this.labelledTextbox4.Name = "labelledTextbox4";
            this.labelledTextbox4.Padding = new System.Windows.Forms.Padding(3);
            this.labelledTextbox4.Size = new System.Drawing.Size(133, 53);
            this.labelledTextbox4.TabIndex = 4;
            // 
            // labelledTextbox5
            // 
            this.labelledTextbox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelledTextbox5.EditText = "";
            this.labelledTextbox5.EditWidth = 87;
            this.labelledTextbox5.LabelText = "Calls Per Day";
            this.labelledTextbox5.Location = new System.Drawing.Point(256, 0);
            this.labelledTextbox5.MaxLength = 32767;
            this.labelledTextbox5.MinimumSize = new System.Drawing.Size(50, 46);
            this.labelledTextbox5.Name = "labelledTextbox5";
            this.labelledTextbox5.Padding = new System.Windows.Forms.Padding(3);
            this.labelledTextbox5.Size = new System.Drawing.Size(123, 53);
            this.labelledTextbox5.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 145);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(383, 36);
            this.panel3.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(191, 36);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Is SysOp";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(191, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(191, 36);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Remote Maintenance";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // AccessGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 229);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelledTextbox2);
            this.Controls.Add(this.labelledTextbox1);
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
        private User_Controls.LabelledTextbox labelledTextbox1;
        private User_Controls.LabelledTextbox labelledTextbox2;
        private System.Windows.Forms.Panel panel2;
        private User_Controls.LabelledTextbox labelledTextbox5;
        private User_Controls.LabelledTextbox labelledTextbox4;
        private User_Controls.LabelledTextbox labelledTextbox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}