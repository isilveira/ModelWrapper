using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using ModelWrapper.Interfaces;
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
        private string ModelName { get; set; }
        public TModel Model { get; set; }
        public List<NewWrapProperty> AllProperties { get; set; }
        public List<PropertyInfo> KeyProperties { get; set; }
        public List<PropertyInfo> SupressedProperties { get; set; }
        public List<PropertyInfo> SupressedResponseProperties { get; set; }
        public List<PropertyInfo> SuppliedProperties { get; set; }
        public List<PropertyInfo> ResponseProperties { get; set; }
        public Dictionary<string, object> RequestObject { get; set; }
        protected WrapRequest()
        {
            Initialize();
        }

        private void Initialize()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"), "WrapRequest/Initialize");
            Model = Activator.CreateInstance<TModel>();
            AllProperties = new List<NewWrapProperty>();
            KeyProperties = new List<PropertyInfo>();
            SupressedProperties = new List<PropertyInfo>();
            SupressedResponseProperties = new List<PropertyInfo>();
            SuppliedProperties = new List<PropertyInfo>();
            ResponseProperties = new List<PropertyInfo>();
            RequestObject = new Dictionary<string, object>();
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"), "WrapRequest/Initialize");
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

        public void ConfigKeys(Expression<Func<TModel, object>> expression)
        {
            KeyProperties.Add(typeof(TModel).GetProperties().Where(p => p.Name.Equals(GetPropertyName(expression))).SingleOrDefault());
        }
        public void ConfigSuppressedProperties(Expression<Func<TModel, object>> expression)
        {
            SupressedProperties.Add(typeof(TModel).GetProperties().Where(p => p.Name.Equals(GetPropertyName(expression))).SingleOrDefault());
        }
        public void ConfigSuppressedResponseProperties(Expression<Func<TModel, object>> expression)
        {
            SupressedResponseProperties.Add(typeof(TModel).GetProperties().Where(p => p.Name.Equals(GetPropertyName(expression))).SingleOrDefault());
        }

        public string GetPropertyName(Expression<Func<TModel, object>> property)
        {
            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return ((PropertyInfo)memberExpression.Member).Name;
        }

        public Dictionary<string, object> GetRequestAsDictionary()
        {
            return RequestObject;
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
                if (propertyValue != propertyEmptyValue && !SuppliedProperties.ToList().Exists(p => p.Name == property.Name))
                {
                    SuppliedProperties.Add(property);
                }
            });
        }

        public virtual void ProcessBind()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"),"ProcessBind");
            SetRoutePropertiesOnRequest();
            SetResponsePropertiesOnRequest();
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"), "ProcessBind");
        }

        private void SetRoutePropertiesOnRequest()
        {
            var RouteProperties = AllProperties.Where(x => x.Source == WrapPropertySource.FromRoute).ToList();

            if (RouteProperties.Any())
            {                
                RequestObject.Add(
                    nameof(RouteProperties),
                    new Dictionary<string,object>(
                        RouteProperties.Select(property => new KeyValuePair<string, object>(property.Name, property.Value)).ToList()
                    )
                );
            }
        }

        private void SetResponsePropertiesOnRequest()
        {
            var responseProperties = AllProperties.Where(x =>
                x.Source == WrapPropertySource.FromQuery
                && x.Name.ToLower().Equals(nameof(ResponseProperties).ToLower())
            ).ToList();

            if (!responseProperties.Any())
            {
                ResponseProperties.AddRange(typeof(TModel).GetProperties().Where(p =>
                       !SupressedProperties.Any(x => x.Name == p.Name)
                       && !SupressedResponseProperties.Any(x => x.Name == p.Name)
                   ).ToList());
            }
            else
            {
                ResponseProperties.AddRange(typeof(TModel).GetProperties().Where(p =>
                    !SupressedProperties.Any(x => x.Name == p.Name)
                    && !SupressedResponseProperties.Any(x => x.Name == p.Name)
                    && responseProperties.Any(x => x.Value.ToString().ToLower().Equals(p.Name.ToLower()))
                ).ToList());
            }

            if (ResponseProperties.Any())
            {
                RequestObject.Add(nameof(ResponseProperties), ResponseProperties.Select(x => x.Name));
            }
        }
    }
}
