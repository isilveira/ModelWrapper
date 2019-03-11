using ModelWrapper.Core;
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
    public class Wrap<TModel> : DynamicObject, IWrap<TModel>
        where TModel : class
    {
        #region Properties
        /// <summary>
        /// Internal model object that hold the supplied value.
        /// </summary>
        private TModel InternalObject { get; set; }

        /// <summary>
        /// List of properties of the model wrapped.
        /// </summary>
        private List<PropertyInfo> AllProperties { get; set; }

        /// <summary>
        /// List of supplied properties of the model wrapped.
        /// </summary>
        private List<PropertyInfo> SuppliedProperties { get; set; }

        /// <summary>
        /// Dictionary with all supplied data.
        /// </summary>
        private Dictionary<string, object> Attributes;
        private List<MemberExpression> MemberExpressions;
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
            Attributes.ToList().ForEach(pair => SetPropertyValue(pair));
        }
        #endregion
        
        #region Wrapper public methods
        /// <summary>
        /// Method that deliver the dictionary of wrapper.
        /// </summary>
        /// <returns>Dictionary with values of wrapper</returns>
        public Dictionary<string, object> AsDictionary()
        {
            return Attributes;
        }

        public void SuppressProperty(MemberExpression expression)
        {
            if (MemberExpressions == null)
                MemberExpressions = new List<MemberExpression>();

            MemberExpressions.Add(expression);
        }

        /// <summary>
        /// Method that patch data into the model object
        /// </summary>
        /// <param name="model">Model object</param>
        /// <returns>Return model object</returns>
        public TModel Patch(TModel model)
        {
            SuppliedProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));
            return model;
        }

        /// <summary>
        /// Method that put data into the model object
        /// </summary>
        /// <param name="model">Model object</param>
        /// <returns>Return model object</returns>
        public TModel Put(TModel model)
        {
            AllProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));
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
            return Attributes.TryGetValue(binder.Name, out result);
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

            SetPropertyValue(Attributes.SingleOrDefault(x => x.Key.Equals(binder.Name)));

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
            SuppliedProperties = new List<PropertyInfo>();
            AllProperties = typeof(TModel).GetProperties().ToList();
            Attributes = new Dictionary<string, object>();
            MemberExpressions = new List<MemberExpression>();
        }

        /// <summary>
        /// Method used to set property value
        /// </summary>
        /// <param name="token">Dictionary key value pair</param>
        private void SetPropertyValue(KeyValuePair<string, object> token)
        {
            var property = typeof(TModel).GetProperties().SingleOrDefault(p => p.Name.ToLower().Equals(token.Key.ToLower()));

            if (property != null)
            {
                SuppliedProperties.Add(property);
                var newPropertyValue = (token.Value is JToken) ? JsonConvert.DeserializeObject(token.Value.ToString(), property.PropertyType) : Convert.ChangeType(token.Value, property.PropertyType);
                property.SetValue(this.InternalObject, newPropertyValue);
            }
        } 
        #endregion
    }
}
