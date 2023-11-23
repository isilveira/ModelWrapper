using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ModelWrapper.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModelWrapper.Models
{
    internal class SelectedProperty : ICloneable
    {
        public string RequestedPropertyName { get; set; }
        public string PropertyName { get; set; }
        public string RootPropertyName { get; set; }
        public Type PropertyType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public SelectedProperty TypedClone()
        {
            return (SelectedProperty)this.Clone();
        }
    }

    internal class SelectedModel
    {
        public string RequestedName { get; set; }
        public string Name { get; set; }
        public Type OriginalType { get; set; }
        public PropertyInfo OriginalPropertyInfo { get; set; }
        public List<SelectedModel> Properties { get; set; }
		public SelectedModel()
		{
            Properties = new List<SelectedModel>();
		}
		public string GetNewTypeName()
		{
			return "SelectWrap" + TypesHelper.GetEntityTypeFromComplex(this.OriginalType).Name + this.OriginalPropertyInfo != default ? this.Name : string.Empty;
		}
	}
}
