using System;
using System.Collections.Generic;
using System.Linq;
using Net_Data.Models;
using Net_Logger;

namespace Net_Data
{
    public partial class BBSDataCore
    {
        public Field GetField(int? userId, string fieldName)
        {
            Field result = null;
            try
            {
                result =  _bbsDataContext.Fields.FirstOrDefault(p => p.UserId==userId && p.FieldName.ToUpper()==fieldName.ToUpper());
            }
            catch (Exception ex)
            {
                LoggingAPI.Error("Exception: ", ex);
                result = null;
            }
            return result;
        }

        public string GetFieldValue(int? userId, string fieldName)
        {
            var field = GetField(userId, fieldName);
            return (field == null)? "":field?.FieldContents;
        }

        public void SetField(Field field)
        {
            if (field.Id == 0)
            {
                _bbsDataContext.Fields.Add(field);
            } 
            _bbsDataContext.SaveChanges();
        }

        public void SetField(int? userId, string fieldName, string fieldContent)
        {
            var field = GetField(userId, fieldName);
            if (field == null)
            {
                field = new Field() { UserId = userId, FieldName = fieldName };
            }
            field.FieldContents = fieldContent;
            SetField(field);
        }

        public List<Field> GetAllFieldsWithFieldName(string fieldName)
        {
            return _bbsDataContext.Fields.Where(p => p.FieldName.ToUpper() == fieldName.ToUpper()).ToList();
        }

    }
}
