namespace SixNet
{
    partial class ChatForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbEntry = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btEndChat = new System.Windows.Forms.Button();
            this.btKickUser = new System.Windows.Forms.Button();
            this.MonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAccess = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btSendFile = new System.Windows.Forms.Button();
            this.btSaveLog = new System.Windows.Forms.Button();
            this.tbDisplay = new System.Windows.Forms.RichTextBox();
            this.btCloseWindow = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbEntry);
            this.panel1.Controls.Add(this.btSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 49);
            this.panel1.TabIndex = 5;
            // 
            // tbEntry
            // 
            this.tbEntry.Location = new System.Drawing.Point(12, 14);
            this.tbEntry.Name = "tbEntry";
            this.tbEntry.Size = new System.Drawing.Size(403, 20);
            this.tbEntry.TabIndex = 1;
            this.tbEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEntry_KeyPress);
            this.tbEntry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbEntry_KeyUp);
            // 
            // btSend
            // 
            this.btSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btSend.Location = new System.Drawing.Point(428, 0);
            this.btSend.Margin = new System.Windows.Forms.Padding(3, 3, 22, 3);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 49);
            this.btSend.TabIndex = 0;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btCloseWindow);
            this.panel2.Controls.Add(this.btSaveLog);
            this.panel2.Controls.Add(this.btSendFile);
            this.panel2.Controls.Add(this.btEndChat);
            this.panel2.Controls.Add(this.btKickUser);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(393, 68);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 259);
            this.panel2.TabIndex = 6;
            // 
            // btEndChat
            // 
            this.btEndChat.Location = new System.Drawing.Point(6, 6);
            this.btEndChat.Name = "btEndChat";
            this.btEndChat.Size = new System.Drawing.Size(97, 25);
            this.btEndChat.TabIndex = 2;
            this.btEndChat.Text = "End Chat";
            this.btEndChat.UseVisualStyleBackColor = true;
            this.btEndChat.Click += new System.EventHandler(this.button2_Click);
            // 
            // btKickUser
            // 
            this.btKickUser.Location = new System.Drawing.Point(6, 34);
            this.btKickUser.Name = "btKickUser";
            this.btKickUser.Size = new System.Drawing.Size(97, 25);
            this.btKickUser.TabIndex = 1;
            this.btKickUser.Text = "Kick User";
            this.btKickUser.UseVisualStyleBackColor = true;
            this.btKickUser.Click += new System.EventHandler(this.button1_Click);
            // 
            // MonitorTimer
            // 
            this.MonitorTimer.Interval = 500;
            this.MonitorTimer.Tick += new System.EventHandler(this.MonitorTimer_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblAccess);
            this.panel3.Controls.Add(this.lblUsername);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 68);
            this.panel3.TabIndex = 7;
            // 
            // lblAccess
            // 
            this.lblAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccess.Location = new System.Drawing.Point(82, 35);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(171, 21);
            this.lblAccess.TabIndex = 11;
            this.lblAccess.Text = "Access:";
            this.lblAccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(82, 9);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(171, 21);
            this.lblUsername.TabIndex = 10;
            this.lblUsername.Text = "User:";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Access:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "User:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btSendFile
            // 
            this.btSendFile.Location = new System.Drawing.Point(6, 62);
            this.btSendFile.Name = "btSendFile";
            this.btSendFile.Size = new System.Drawing.Size(97, 25);
            this.btSendFile.TabIndex = 3;
            this.btSendFile.Text = "Send Textfile";
            this.btSendFile.UseVisualStyleBackColor = true;
            this.btSendFile.Click += new System.EventHandler(this.btSendFile_Click);
            // 
            // btSaveLog
            // 
            this.btSaveLog.Location = new System.Drawing.Point(6, 118);
            this.btSaveLog.Name = "btSaveLog";
            this.btSaveLog.Size = new System.Drawing.Size(97, 25);
            this.btSaveLog.TabIndex = 4;
            this.btSaveLog.Text = "Save Log";
            this.btSaveLog.UseVisualStyleBackColor = true;
            this.btSaveLog.Click += new System.EventHandler(this.btSaveLog_Click);
            // 
            // tbDisplay
            // 
            this.tbDisplay.BackColor = System.Drawing.Color.Black;
            this.tbDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDisplay.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDisplay.ForeColor = System.Drawing.Color.White;
            this.tbDisplay.Location = new System.Drawing.Point(0, 68);
            this.tbDisplay.Name = "tbDisplay";
            this.tbDisplay.ReadOnly = true;
            this.tbDisplay.Size = new System.Drawing.Size(393, 259);
            this.tbDisplay.TabIndex = 8;
            this.tbDisplay.Text = "";
            // 
            // btCloseWindow
            // 
            this.btCloseWindow.Location = new System.Drawing.Point(6, 146);
            this.btCloseWindow.Name = "btCloseWindow";
            this.btCloseWindow.Size = new System.Drawing.Size(97, 25);
            this.btCloseWindow.TabIndex = 5;
            this.btCloseWindow.Text = "Close Window";
            this.btCloseWindow.UseVisualStyleBackColor = true;
            this.btCloseWindow.Click += new System.EventHandler(this.button5_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Send RAW";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 376);
            this.ControlBox = false;
            this.Controls.Add(this.tbDisplay);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "ChatForm";
            this.Text = "Chatting with user";
            this.Load += new System.EventHandler(this.OutputForm_Load);
            this.Resize += new System.EventHandler(this.ChatForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbEntry;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btKickUser;
        private System.Windows.Forms.Timer MonitorTimer;
        private System.Windows.Forms.Button btEndChat;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAccess;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSaveLog;
        private System.Windows.Forms.Button btSendFile;
        private System.Windows.Forms.RichTextBox tbDisplay;
        private System.Windows.Forms.Button btCloseWindow;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}