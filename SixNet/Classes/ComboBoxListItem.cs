using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixNet_GUI.Classes
{
    public class ComboBoxListItem
    {
        public int ItemId { get; set; }
        public string ItemText { get; set; }

        public ComboBoxListItem() { }
        public ComboBoxListItem(int itemId, string itemText) : this()
        {
            ItemId = itemId;
            ItemText = itemText;
        }
    }
}
