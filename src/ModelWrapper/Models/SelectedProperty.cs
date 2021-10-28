using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModelWrapper.Models
{
    internal class SelectedProperty
    {
        public string RequestedPropertyName { get; set; }
        public string PropertyName { get; set; }
        public string RootPropertyName { get; set; }
        public bool IsFromInnerObject { get; set; }
        public bool IsFromCollectionObject { get; set; }
        public Type PropertyType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }

    internal class SelectedModel
    {
        public string RequestedName { get; set; }
        public string Name { get; set; }
        public Type OriginalType { get; set; }
        public PropertyInfo OriginalPropertyInfo { get; set; }
        public List<SelectedModel> Properties { get; set; }
        public bool IsClass { get { return Properties.Count > 0; } }
        public bool IsCollection { get; set; }
        public SelectedModel()
        {
            Properties = new List<SelectedModel>();
        }
    }
}
