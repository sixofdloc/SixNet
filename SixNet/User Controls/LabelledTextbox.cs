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

        private bool _editable = true;

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value; 
        }

        public string EditText
        {
            get => textBox1.Text;
            set => SetEditText(value);
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

        public bool IsPassword
        {
            get => (textBox1.PasswordChar == '*');
            set => textBox1.PasswordChar = value ? '*' : '\0';
        }

        public bool Editable
        {
            get => _editable;
            set => SetEditable(value);
        }

        private void SetEditable(bool editable)
        {
            _editable = editable;
            textBox1.Visible = editable;
            label2.Visible = !editable;
            textBox1.Dock = editable ? DockStyle.Bottom : DockStyle.None;
            label2.Dock = editable ? DockStyle.None : DockStyle.Bottom;
        }

        private void SetEditText(string editText)
        {
            textBox1.Text = editText;
            label2.Text = editText;
        }

        public bool NumbersOnly { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public bool AllowEmpty { get; set; }
        public bool AutoValidate { get; set; }
            
        public LabelledTextbox()
        {
            InitializeComponent();
            NumbersOnly = false;
            AllowEmpty = true;
            SetEditable(true);
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
            if (AutoValidate)
            {
                if (NumbersOnly)
                {
                    NumericValidation();
                }
                else
                {
                    if (BasicValidation())
                        if (this.Edit_Finished != null) this.Edit_Finished(this, e);
                }
            }
        }

        public bool NumericValidation()
        {
            bool b = true;
            int i = 0;
            if (Int32.TryParse(textBox1.Text, out i))
            {
                if (i <= Min || i >= Max)
                {
                    MessageBox.Show(label1.Text + " must be a number between " + Min + " and " + Max);
                    b = false;
                    this.Focus();
                }
            }
            else
            {
                MessageBox.Show(label1.Text + " must be a number.");
                b = false;
                this.Focus();
            }
            return b;
        }

        public bool BasicValidation()
        {
            bool b = true;
            int i = 0;
            if (!AllowEmpty)
            {
                if (textBox1.Text.Length == 0)
                {
                    if (i <= Min || i >= Max)
                    {
                        MessageBox.Show(label1.Text + " must have a value.");
                        b = false;
                        this.Focus();
                    }
                }
            }
            return b;
        }

    }
}
