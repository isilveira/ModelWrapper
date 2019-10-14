using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using ModelWrapper.Extensions;
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
        public Dictionary<string, List<string>> ConfigProperties { get; set; }
        public Dictionary<string, object> ConfigValues { get; set; }
        public Dictionary<string, object> RequestObject { get; set; }
        protected WrapRequest()
        {
            Initialize();
        }

        private void Initialize()
        {
            Model = Activator.CreateInstance<TModel>();
            AllProperties = new List<WrapRequestProperty>();
            ConfigProperties = new Dictionary<string, List<string>>();
            ConfigValues = new Dictionary<string, object>();
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
        private void SetConfigProperty(string name, string property)
        {
            var configProperties = ConfigProperties.Where(x => x.Key == name).SingleOrDefault();

            if (configProperties.IsDefault())
            {
                ConfigProperties.Add(name, new List<string>{ property });
            }
            else
            {
                configProperties.Value.Add(property);
            }
        }
        private void SetConfigValues(string name, object value)
        {
            var configValues = ConfigValues.Where(x => x.Key == name).SingleOrDefault();

            if (configValues.IsDefault())
            {
                ConfigValues.Add(name, value);
            }
            else
            {
                ConfigValues.Remove(name);
                ConfigValues.Add(name, value);
            }
        }
        public void ConfigKeys(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_KEYS,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name.ToCamelCase()
            );
        }
        public void ConfigDefaultReturnedCollectionSize(int defaultReturnCollectionSize)
        {
            SetConfigValues(
                Constants.CONST_DEFAULT_COLLECTION_SIZE,
                defaultReturnCollectionSize
            );
        }
        public void ConfigMaxReturnedCollectionSize(int maxReturnCollectionSize)
        {
            SetConfigValues(
                Constants.CONST_MAX_COLLECTION_SIZE,
                maxReturnCollectionSize
            );
        }
        public void ConfigMinReturnedCollectionSize(int minReturnCollectionSize)
        {
            SetConfigValues(
                Constants.CONST_MIN_COLLECTION_SIZE,
                minReturnCollectionSize
            );
        }
        public void ConfigSuppressedProperties(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_SUPRESSED,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name.ToCamelCase()
            );
        }
        public void ConfigSuppressedResponseProperties(Expression<Func<TModel, object>> expression)
        {
            SetConfigProperty(
                Constants.CONST_SUPPRESSED_RESPONSE,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdaHelper.GetPropertyName(expression))).SingleOrDefault().Name.ToCamelCase()
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
                if (propertyValue != propertyEmptyValue && !ConfigProperties.GetValue(Constants.CONST_SUPPLIED).ToList().Exists(p => p == property.Name))
                {
                    SetConfigProperty(Constants.CONST_SUPPLIED, property.Name);
                }
            });
        }

        public void BindComplete()
        {
            var routeProperties = new Dictionary<string, object>();
            
            foreach(var routeProperty in AllProperties.Where(x => x.Source == WrapPropertySource.FromRoute))
            {
                routeProperties.Add(routeProperty.Name.ToCamelCase(), routeProperty.Value);
            }

            RequestObject.Add(Constants.CONST_ROUTE, routeProperties);
        }
    }
}
