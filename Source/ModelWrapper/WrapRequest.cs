using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper
{
    [ModelBinder(BinderType = typeof(WrapRequestBinder))]
    public class WrapRequest<TModel> : DynamicObject, IWrapRequest<TModel>
        where TModel : class
    {
        public TModel Model { get; set; }
        public List<WrapRequestProperty> AllProperties { get; set; }
        public List<ConfigProperties> ConfigProperties { get; set; }
        public Dictionary<string, object> RequestObject { get; set; }
        protected WrapRequest()
        {
            Initialize();
        }

        private void Initialize()
        {
            Model = Activator.CreateInstance<TModel>();
            AllProperties = new List<WrapRequestProperty>();
            ConfigProperties = new List<ConfigProperties>();
            RequestObject = new Dictionary<string, object>();
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
            var source = binder.GetType() == typeof(WrapRequestMemberBinder) ? ((WrapRequestMemberBinder)binder).Source : WrapPropertySource.FromBody;
            AllProperties.Add(new WrapRequestProperty { Name = binder.Name, Value = value, Source = source });

            SetPropertyValue(binder.Name, value);

            return true;
        }
        #endregion


        #region Configuration methods
        private List<string> GetConfigProperty(string name)
        {
            var configProperties = ConfigProperties.Where(x => x.Name == name).SingleOrDefault();
            return configProperties != null ? configProperties.Properties : new List<string>();
        }
        private void SetConfigProperty(string name, string property)
        {
            var configProperties = ConfigProperties.Where(x => x.Name == name).SingleOrDefault();

            if (configProperties == null)
            {
                configProperties = new ConfigProperties { Name = name, Properties = new List<string> { property } };
                ConfigProperties.Add(configProperties);
            }
            else
            {
                configProperties.Properties.Add(property);
            }
        }
        public void ConfigKeys(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_KEYS,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name
            );
        }
        public void ConfigSuppressedProperties(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_SUPRESSED,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name
            );
        }
        public void ConfigSuppressedResponseProperties(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_SUPPRESSED_RESPONSE,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name
            );
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

                SetConfigProperty(Constants.CONST_SUPPLIED, property.Name);
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
        public TResult Project<TResult>(Func<TModel, TResult> function)
        {
            TResult value = function.Invoke(this.Model);
            UpdateSuppliedProperties();
            return value;
        }
        public void Project(Action<TModel> action)
        {
            action.Invoke(this.Model);
            UpdateSuppliedProperties();
        }
        private void UpdateSuppliedProperties()
        {
            typeof(TModel).GetProperties().ToList().ForEach(property =>
            {
                var propertyValue = GetPropertyValue(property.Name);
                var propertyEmptyValue = GetPropertyValue(property.Name, true);
                if (propertyValue != propertyEmptyValue && !GetConfigProperty(Constants.CONST_SUPPLIED).ToList().Exists(p => p == property.Name))
                {
                    SetConfigProperty(Constants.CONST_SUPPLIED, property.Name);
                }
            });
        }
    }
}
