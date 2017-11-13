using System;
using System.Windows.Forms;

namespace SixNet.Forms
{
    public partial class MessagePrompt : Form
    {
        public MessagePrompt()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
