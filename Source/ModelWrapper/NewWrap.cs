using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModelWrapper
{
    [ModelBinder(BinderType = typeof(WrapBinder))]
    public class NewWrap<TModel> : DynamicObject
        where TModel : class
    {
        private string ModelName { get; set; }
        public TModel Model { get; set; }
        public IList<NewWrapProperty> AllProperties { get; set; }
        private IList<PropertyInfo> KeyProperties { get; set; }
        private IList<PropertyInfo> SupressedProperties { get; set; }
        private IList<PropertyInfo> SuppliedProperties { get; set; }
        private IList<PropertyInfo> ResponseProperties { get; set; }
        protected NewWrap()
        {
            Initialize();
        }

        private void Initialize()
        {
            Model = Activator.CreateInstance<TModel>();
            AllProperties = new List<NewWrapProperty>();
            KeyProperties = new List<PropertyInfo>();
            SupressedProperties = new List<PropertyInfo>();
            SuppliedProperties = new List<PropertyInfo>();
            ResponseProperties = new List<PropertyInfo>();
        }


        #region Access member methods
        /// <summary>
        /// Mothod that overrides the access member get
        /// </summary>
        /// <param name="binder">GetMemberBinder object</param>
        /// <param name="result">Value that will be returned</param>
        /// <returns>bool, if succeeds true</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetPropertyValue(binder.Name);

            return result != null ? true : false;
        }

        /// <summary>
        /// Mothod that overrides the access member set
        /// </summary>
        /// <param name="binder">SetMemberBinder object</param>
        /// <param name="value">Value that will be set</param>
        /// <returns>bool, if succeeds true</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var source = binder.GetType() == typeof(WrapMemberBinder) ? ((WrapMemberBinder)binder).Source : WrapPropertySource.FromBody;
            AllProperties.Add(new NewWrapProperty { Name = binder.Name, Value = value, Source = source });

            SetPropertyValue(binder.Name, value);

            return true;
        }
        #endregion

        internal void SetPropertyValue(string propertyName, object propertyValue)
        {
            var property = Model.GetType().GetProperties().SingleOrDefault(p => p.Name.ToLower().Equals(propertyName.ToLower()));

            if (property != null)
            {
                Type propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(propertyType) != null)
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                SuppliedProperties.Add(property);
                var newPropertyValue = (propertyValue is JToken) ? JsonConvert.DeserializeObject(propertyValue.ToString(), property.PropertyType) : Convert.ChangeType(propertyValue, propertyType);
                property.SetValue(this.Model, newPropertyValue);
            }
        }

        internal object GetPropertyValue(string propertyName, bool empty = false)
        {
            var property = Model.GetType().GetProperties().SingleOrDefault(p => p.Name.ToLower().Equals(propertyName.ToLower()));
            if (empty)
                return property.GetValue(Activator.CreateInstance<TModel>());
            else
                return property.GetValue(this.Model);
        }
    }
}
