using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using ModelWrapper.Core;
using ModelWrapper.Core.Binders.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ModelWrapper
{
    //[ModelBinder(BinderType = typeof(WrapModelBinder))]
    public class Wrap<TModel> : DynamicObject, IWrap<TModel>
        where TModel : class
    {
        private TModel InternalObject { get; set; }
        private List<PropertyInfo> AllProperties { get; set; }
        private List<PropertyInfo> SuppliedProperties { get; set; }

        private Dictionary<string, object> Attributes;

        static Wrap() { }

        private void InitializePrivateObjects()
        {
            InternalObject = Activator.CreateInstance<TModel>();
            SuppliedProperties = new List<PropertyInfo>();
            AllProperties = typeof(TModel).GetProperties().ToList();
            Attributes = new Dictionary<string, object>();
        }

        public Wrap()
        {
            InitializePrivateObjects();
        }
        public Wrap(Dictionary<string, object> attributes)
        {
            InitializePrivateObjects();
            Attributes = attributes;
            Attributes.ToList().ForEach(pair => SetPropertyValue(pair));
        }

        public Dictionary<string, object> AsDictionary()
        {
            return Attributes;
        }

        public TModel Patch(TModel model)
        {
            SuppliedProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));
            return model;
        }

        public TModel Put(TModel model)
        {
            AllProperties.ForEach(property => property.SetValue(model, property.GetValue(InternalObject)));
            return model;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Attributes.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Attributes.Add(binder.Name, value);

            SetPropertyValue(Attributes.SingleOrDefault(x => x.Key.Equals(binder.Name)));

            return true;
        }

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
    }
}
