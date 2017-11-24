using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SixNet_BBS_Data;
using SixNet_GUI.Classes;
using SixNet_Logger;

namespace SixNet_GUI.User_Controls
{
    public partial class LabelledSelector : UserControl
    {
        private List<ComboBoxListItem> _itemList;

        public bool NoneOption { get; set; }

        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public ComboBoxListItem SelectedItem
        {
            get => (ComboBoxListItem)(comboBox1.SelectedItem);
            set => comboBox1.SelectedItem = value;
        }

        public int SelectedValue
        {
            get => comboBox1.SelectedItem==null?-1:((ComboBoxListItem)(comboBox1.SelectedItem)).ItemId;
            set => comboBox1.SelectedValue = value;
        }


        public LabelledSelector()
        {
            InitializeComponent();
        }

        public bool BasicValidation()
        {
            bool b = true;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show(label1.Text + " must have a value.");
                b = false;
                this.Focus();
            }
            return b;
        }

        public void Populate(List<ComboBoxListItem> itemList)
        {
            _itemList = itemList;
            RefreshList();
        }

        public bool RefreshList()
        {
            var result = false;
            try
            {
                comboBox1.Items.Clear();
                var noneItem = new ComboBoxListItem(-1, "None");
                if (NoneOption) comboBox1.Items.Add(noneItem);
                
                foreach (ComboBoxListItem item in _itemList)
                {
                    comboBox1.Items.Add(item);
                }
                comboBox1.DisplayMember = "ItemText";
                comboBox1.ValueMember = "ItemId";
                if (NoneOption)
                {
                    comboBox1.SelectedItem = noneItem;
                } else
                {
                    if (comboBox1.Items.Count > 0) comboBox1.SelectedItem = comboBox1.Items[0];
                }
            }
            catch (Exception e)
            {
                LoggingAPI.Error(e);
            }
            return result;
        }

    }
}
