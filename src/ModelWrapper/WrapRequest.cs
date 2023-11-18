using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using ModelWrapper.Extensions;
using ModelWrapper.Helpers;
using ModelWrapper.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace ModelWrapper
{
    /// <summary>
    /// Class that encapsulates the request
    /// </summary>
    /// <typeparam name="TModel">Generic type of the endpoint</typeparam>
    [ModelBinder(BinderType = typeof(WrapRequestBinder))]
    public class WrapRequest<TModel> : DynamicObject
        where TModel : class
    {
        /// <summary>
        /// Instance of the Model
        /// </summary>
        internal TModel Model { get; set; }
        /// <summary>
        /// List of properties received on request
        /// </summary>
        internal List<WrapRequestProperty> AllProperties { get; set; }
        /// <summary>
        /// List of configurations for the request
        /// </summary>
        internal Dictionary<string, List<string>> ConfigProperties { get; set; }
        /// <summary>
        /// List of configuration values
        /// </summary>
        internal Dictionary<string, object> ConfigValues { get; set; }
        /// <summary>
        /// Representation of the request object
        /// </summary>
        internal Dictionary<string, object> RequestObject { get; set; }
        /// <summary>
        /// Class protected constructor
        /// </summary>
        protected WrapRequest()
        {
            Initialize();
		}
		/// <summary>
		/// Class protected constructor
		/// </summary>
		protected WrapRequest(TModel model)
		{
			Initialize();
            this.Model = model;
            UpdateSuppliedProperties();
		}
		/// <summary>
		/// Method that instantiates the properties
		/// </summary>
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
        public override bool TryGetMember(
            GetMemberBinder binder,
            out object result
        )
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
        public override bool TrySetMember(
            SetMemberBinder binder,
            object value
        )
        {
            var source = binder.GetType() == typeof(WrapRequestMemberBinder) ? ((WrapRequestMemberBinder)binder).Source : WrapPropertySource.FromBody;
            
            AddProperty(binder.Name, value, source);

            SetPropertyValue(binder.Name, value);

            return true;
        }
        public void AddProperty(string name, object value, WrapPropertySource source = WrapPropertySource.FromBody)
        {
            AllProperties.Add(new WrapRequestProperty { Name = name, Value = value, Source = source });
        }
        #endregion
        #region Configuration methods
        /// <summary>
        /// Method that sets configuration properties into ConfigProperties list
        /// </summary>
        /// <param name="name">Configuration name</param>
        /// <param name="property">Property name</param>
        private void SetConfigProperty(
            string name,
            string property
        )
        {
            var configProperties = ConfigProperties.Where(x => x.Key == name).SingleOrDefault();

            if (configProperties.IsDefault())
            {
                ConfigProperties.Add(name, new List<string> { property });
            }
            else
            {
                configProperties.Value.Add(property);
            }
        }
        /// <summary>
        /// Method that stes configuration values into ConfigValues List
        /// </summary>
        /// <param name="name">Configuration name</param>
        /// <param name="value">Value</param>
        private void SetConfigValues(
            string name,
            object value
        )
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
        /// <summary>
        /// Method that sets configuration keys into ConfigKeys list
        /// </summary>
        /// <param name="expression">Lambda expression for the key</param>
        public void ConfigKeys(
            Expression<Func<TModel, object>> expression
        )
        {
            SetConfigProperty(
                Constants.CONST_KEYS,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdasHelper.GetPropertyName(expression))).SingleOrDefault().Name.ToCamelCase()
            );
        }
        /// <summary>
        /// Method that sets the default return size of collections
        /// </summary>
        /// <param name="defaultReturnSizeOfCollections">Default return size of collections</param>
        public void ConfigDefaultReturnSizeOfCollections(
            int defaultReturnSizeOfCollections
        )
        {
            SetConfigValues(
                Constants.CONST_DEFAULT_COLLECTION_SIZE,
                defaultReturnSizeOfCollections
            );
        }
        /// <summary>
        /// Method that sets the maximum return size of collections
        /// </summary>
        /// <param name="maximumReturnSizeOfCollections">Maximum return size of collections</param>
        public void ConfigMaxReturnSizeOfCollections(
            int maximumReturnSizeOfCollections
        )
        {
            SetConfigValues(
                Constants.CONST_MAX_COLLECTION_SIZE,
                maximumReturnSizeOfCollections
            );
        }
        /// <summary>
        /// Method that sets the minimum return size of collections
        /// </summary>
        /// <param name="minimumReturnSizeOfCollection">Minimum return size of collections</param>
        public void ConfigMinimumReturnSizeOfCollection(
            int minimumReturnSizeOfCollection
        )
        {
            SetConfigValues(
                Constants.CONST_MIN_COLLECTION_SIZE,
                minimumReturnSizeOfCollection
            );
        }
        /// <summary>
        /// Method that configures suppressed properties
        /// </summary>
        /// <param name="expression">Property lambda expression</param>
        public void ConfigSuppressedProperties(
            Expression<Func<TModel, object>> expression
        )
        {
            SetConfigProperty(
                Constants.CONST_SUPRESSED,
                typeof(TModel).GetProperties().Where(p => p.Name.Equals(LambdasHelper.GetPropertyName(expression))).SingleOrDefault().Name.ToCamelCase()
            );
        }
        /// <summary>
        /// Method that configures suppressed properties
        /// </summary>
        /// <param name="property">Property</param>
        public void ConfigSuppressedProperties(
            string property
        )
        {
            SetConfigProperty(
                Constants.CONST_SUPRESSED,
                property
            );
        }
        /// <summary>
        /// Method that configures suppressed response properties
        /// </summary>
        /// <param name="expression">Property lambda expression</param>
        public void ConfigSuppressedResponseProperties(
            Expression<Func<TModel, object>> expression
        )
        {
            var expressionPropertyName = LambdasHelper.GetPropertyName(expression);
            SetConfigProperty(
                Constants.CONST_SUPPRESSED_RESPONSE,
                expressionPropertyName
            );
        }
        /// <summary>
        /// Method that configures suppressed response properties
        /// </summary>
        /// <param name="property">Property</param>
        public void ConfigSuppressedResponseProperties(
            string property
        )
        {
            SetConfigProperty(
                Constants.CONST_SUPPRESSED_RESPONSE,
                property
            );
        }
        #endregion
        #region Internal Property Access
        /// <summary>
        /// Method that sets a property value into model object
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyValue">Property value</param>
        internal void SetPropertyValue(
            string propertyName,
            object propertyValue
        )
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
                if (propertyValue is JToken)
                {
                    var newPropertyValue = JsonConvert.DeserializeObject(propertyValue.ToString(), property.PropertyType);
                    property.SetValue(this.Model, newPropertyValue);
                }
                else if (propertyValue != null)
                {
                    bool changed = false;
                    var newPropertyValue = TypesHelper.TryChangeType(propertyValue.ToString(), property.PropertyType, out changed);
                    if (changed)
                    {
                        property.SetValue(this.Model, newPropertyValue);
                    }
                }
                else
                {
                    property.SetValue(this.Model, propertyValue);
                }
            }
        }
        /// <summary>
        /// Method that gets a property value from model object
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="empty">Indication if property value must be empty</param>
        /// <returns>Property value</returns>
        internal object GetPropertyValue(
            string propertyName,
            bool empty = false
        )
        {
            var property = Model.GetType().GetProperties().SingleOrDefault(p => p.Name.ToLower().Equals(propertyName.ToLower()));
            if (empty)
                return property.GetValue(Activator.CreateInstance<TModel>());
            else
                return property.GetValue(this.Model);
        }
        #endregion
        #region Model projection methods
        /// <summary>
        /// Method that projects values or functions of the model object
        /// </summary>
        /// <typeparam name="TResult">Projection result type</typeparam>
        /// <param name="function">Lambda expression for the function</param>
        /// <returns>Projection result</returns>
        public TResult Project<TResult>(
            Func<TModel, TResult> function
        )
        {
            TResult value = function.Invoke(this.Model);
            UpdateSuppliedProperties();
            return value;
		}
		/// <summary>
		/// Method that projects values or functions of the model object
		/// </summary>
		/// <typeparam name="TResult">Projection result type</typeparam>
		/// <param name="function">Lambda expression for the function</param>
		/// <returns>Projection result</returns>
		public WrapRequest<TModel> Project(TModel model)
		{
            this.Model = model;
			UpdateSuppliedProperties();
			return this;
		}
		/// <summary>
		/// Method that projects an action on the model object
		/// </summary>
		/// <param name="action">Lambda expression for the action</param>
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
                if (((propertyValue != null && !propertyValue.Equals(propertyEmptyValue))
                        || (propertyValue == null && propertyValue != propertyEmptyValue)
                    ) && !ConfigProperties.GetValue(Constants.CONST_SUPPLIED).ToList().Exists(p => p == property.Name))
                {
                    SetConfigProperty(Constants.CONST_SUPPLIED, property.Name);
                }
            });
        }
        #endregion
        /// <summary>
        /// Method that sets route values into request object. Invoked at the end of the bind.
        /// </summary>
        public void BindComplete()
        {
            var routeProperties = new Dictionary<string, object>();

            foreach (var routeProperty in AllProperties.Where(x => x.Source == WrapPropertySource.FromRoute))
            {
                routeProperties.Add(routeProperty.Name.ToCamelCase(), routeProperty.Value);
            }

            RequestObject.SetValue(Constants.CONST_ROUTE, routeProperties);
        }
        public TModel GetModel()
        {
            return Model;
        }
        public Dictionary<string, object> GetRequestObject()
        {
            return RequestObject;
        }
    }
}
