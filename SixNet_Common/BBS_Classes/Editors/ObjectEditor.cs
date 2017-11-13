using System;
using System.Collections.Generic;
using System.Reflection;

namespace SixNet_BBS.BBS_Classes.Editors
{
    class ObjectEditor
    {
        List<PropertyInfo> props { get; set; }
        int objectId { get; set; }
        public void EditObject(Object o, string idproperty)
        {
            props = new List<PropertyInfo>();
            foreach (var propertyInfo in o.GetType()
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance))
            {
                if (propertyInfo.Name != idproperty)
                {
                    props.Add(propertyInfo);
                }
                else
                {
                    objectId = (int)(propertyInfo.GetValue(o, null));
                }
            }
        }


    }
}
