using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SixNet_GUI.User_Controls
{
    public partial class LabelledTextbox : UserControl
    {
        public event EventHandler Edit_Finished;

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string EditText
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        public int MaxLength
        {
            get => textBox1.MaxLength;
            set => textBox1.MaxLength = value;
        }

        public int EditWidth
        {
            get => textBox1.Width;
            set => SetTextBoxWidth(value);
        }

        public new bool TabStop
        {
            get => textBox1.TabStop;
            set => textBox1.TabStop = value;
        }

        public new int TabIndex
        {
            get => textBox1.TabIndex;
            set => textBox1.TabIndex = value;
        }


        public LabelledTextbox()
        {
            InitializeComponent();
        }

        private void SetTextBoxWidth(int w)
        {
            if (w == 0)
            {
                textBox1.Dock = DockStyle.Bottom;
            }
            else
            {
                if (textBox1.Dock == DockStyle.Bottom)
                {
                    textBox1.Dock = DockStyle.None;
                }
                textBox1.Width = w;
            }
        }

        private void LabelledTextbox_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.Edit_Finished != null) this.Edit_Finished(this, e);
        }
    }
}
