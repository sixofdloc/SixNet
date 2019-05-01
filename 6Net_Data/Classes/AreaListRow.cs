using System;
using Net_Data.Enums;
using Net_Data.Models;

namespace Net_Data.Classes
{
    public class AreaListRow
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AreaListRowType RowType {get; set;}

        public AreaListRow (TitledModel area, int listId, AreaListRowType rowType) 
        {
            Id = area.Id;
            Title = area.Title;
            Description = area.Description;
            ListId = listId;
            RowType = rowType;
        }

    }
}
