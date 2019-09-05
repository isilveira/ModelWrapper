using Microsoft.AspNetCore.Mvc;
using ModelWrapper.Binders;
using ModelWrapper.Extensions;
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
    public enum EnumProperties
    {
        All,
        AllWithoutKeys,
        OnlySupplieds,
        SuppliedsWithoutKeys
    }

    //[ModelBinder(BinderType =typeof(WrapBinder))]
    public class Wrap<TModel> : DynamicObject
        where TModel : class
    {
        [FromQuery]
        public Dictionary<string, object> QueryStringProperties { get; set; }
        public TModel WrappedModel { get; set; }

        #region OldWrapper
        #region Properties
        internal TModel InternalEmptyObject { get; set; }
        /// <summary>
        /// Internal model object that hold the supplied value.
        /// </summary>
        internal TModel InternalObject { get; set; }

        /// <summary>
        /// List of properties of the model wrapped.
        /// </summary>
        internal List<PropertyInfo> AllProperties { get; set; }

        /// <summary>
        /// List of supplied properties of the model wrapped.
        /// </summary>
        internal List<PropertyInfo> SuppliedProperties { get; set; }

        /// <summary>
        /// Dictionary with all supplied data.
        /// </summary>
        internal Dictionary<string, object> Attributes;
        internal List<Expression<Func<TModel, object>>> SupressedMembers;
        internal List<Expression<Func<TModel, object>>> KeyMembers;
        #endregion

        #region Contructors
        /// <summary>
        /// Wrap constructor without parameters
        /// </summary>
        public Wrap()
        {
            InitializePrivateObjects();
        }

        /// <summary>
        /// Wrap constructor that recieves a dictionary.
        /// </summary>
        /// <param name="attributes">Dictionary with values to create a wrapper</param>
        public Wrap(Dictionary<string, object> attributes)
        {
            InitializePrivateObjects();
            Attributes = attributes;
            Attributes.ToList().ForEach(pair => SetPropertyValue(pair.Key, pair.Value));
        }
        #endregion

        #region Wrapper public methods
        /// <summary>
        /// Method that deliver the dictionary of wrapper.
        /// </summary>
        /// <returns>Dictionary with values of wrapper</returns>
        public Dictionary<string, object> AsDictionary(EnumProperties enumProperties = EnumProperties.All)
        {
            var dictionary = new Dictionary<string, object>();
            var properties = AllProperties.Where(x => !SupressedMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();

            if (enumProperties == EnumProperties.OnlySupplieds || enumProperties == EnumProperties.SuppliedsWithoutKeys)
            {
                properties = properties.Where(property => SuppliedProperties.Exists(x => x.Name == property.Name)).ToList();
            }

            if (enumProperties == EnumProperties.AllWithoutKeys || enumProperties == EnumProperties.SuppliedsWithoutKeys)
            {
                properties = properties.Where(x => !KeyMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();
            }

            properties.ForEach(x =>
                dictionary.Add(x.Name.ToCamelCase(), x.GetValue(this.InternalObject))
            );

            return dictionary;
        }

        public void KeyProperty(Expression<Func<TModel, object>> expression)
        {
            if (KeyMembers == null)
            {
                KeyMembers = new List<Expression<Func<TModel, object>>>();
            }

            KeyMembers.Add(expression);
        }
        public void SuppressProperty(Expression<Func<TModel, object>> expression)
        {
            if (SupressedMembers == null)
            {
                SupressedMembers = new List<Expression<Func<TModel, object>>>();
            }

            SupressedMembers.Add(expression);
        }

        public TResult Project<TResult>(Func<TModel, TResult> function)
        {
            TResult value = function.Invoke(this.InternalObject);
            UpdateSuppliedProperties();
            return value;
        }

        public void Project(Action<TModel> action)
        {
            action.Invoke(this.InternalObject);
            UpdateSuppliedProperties();
        }

        private void UpdateSuppliedProperties()
        {
            foreach (var property in AllProperties)
            {
                var propertyValue = GetPropertyValue(property.Name);
                var propertyEmptyValue = GetPropertyValue(property.Name, true);
                if (propertyValue != propertyEmptyValue && !SuppliedProperties.Exists(p => p.Name == property.Name))
                {
                    SuppliedProperties.Add(property);
                }
            }
        }

        public TModel Post()
        {
            var model = Activator.CreateInstance<TModel>();

            var postProperties = AllProperties.ToList();

            postProperties = postProperties.Where(x => !SupressedMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();
            postProperties = postProperties.Where(x => !KeyMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();

            postProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));

            return model;
        }

        /// <summary>
        /// Method that put data into the model object
        /// </summary>
        /// <param name="model">Model object</param>
        /// <returns>Return model object</returns>
        public TModel Put(TModel model)
        {
            var putProperties = AllProperties.ToList();

            putProperties = putProperties.Where(x => !SupressedMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();
            putProperties = putProperties.Where(x => !KeyMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();

            putProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));

            return model;
        }

        /// <summary>
        /// Method that patch data into the model object
        /// </summary>
        /// <param name="model">Model object</param>
        /// <returns>Return model object</returns>
        public TModel Patch(TModel model)
        {
            var patchtProperties = AllProperties.Where(x => SuppliedProperties.Exists(y => y.Name == x.Name)).ToList();

            patchtProperties = patchtProperties.Where(x => !SupressedMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();
            patchtProperties = patchtProperties.Where(x => !KeyMembers.Any(y => GetPropertyName(y) == x.Name)).ToList();

            patchtProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));

            return model;
        }
        #endregion

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
            Attributes.Add(binder.Name, value);

            SetPropertyValue(binder.Name, value);

            return true;
        }
        #endregion

        #region Wrapper private methods
        /// <summary>
        /// Method that initialize all private objects.
        /// </summary>
        private void InitializePrivateObjects()
        {
            InternalObject = Activator.CreateInstance<TModel>();
            InternalEmptyObject = Activator.CreateInstance<TModel>();
            SuppliedProperties = new List<PropertyInfo>();
            AllProperties = typeof(TModel).GetProperties().ToList();
            Attributes = new Dictionary<string, object>();
            SupressedMembers = new List<Expression<Func<TModel, object>>>();
        }

        internal void SetPropertyValue(string propertyName, object propertyValue)
        {
            var property = AllProperties.SingleOrDefault(p => p.Name.ToLower().Equals(propertyName.ToLower()));

            if (property != null)
            {
                Type propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(propertyType) != null)
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }

                SuppliedProperties.Add(property);
                var newPropertyValue = (propertyValue is JToken) ? JsonConvert.DeserializeObject(propertyValue.ToString(), property.PropertyType) : Convert.ChangeType(propertyValue, propertyType);
                property.SetValue(this.InternalObject, newPropertyValue);
            }
        }

        internal object GetPropertyValue(string propertyName, bool empty = false)
        {
            var property = AllProperties.SingleOrDefault(p => p.Name.ToLower().Equals(propertyName.ToLower()));
            if (empty)
                return property.GetValue(this.InternalEmptyObject);
            else
                return property.GetValue(this.InternalObject);
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
        #endregion 
        #endregion
    }
}
