using System;
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
using Newtonsoft.Json.Linq;

namespace ModelWrapper
{
    [ModelBinder(BinderType = typeof(WrapModelBinder))]
    public class Wrap<T> : DynamicObject, IWrap<T>
        where T : class
    {
        private Dictionary<string, object> Attributes;

        static Wrap() { }
        public Wrap() { if (Attributes == null) Attributes = new Dictionary<string, object>(); }
        public Wrap(Dictionary<string, object> attributes)
        {
            Attributes = attributes;
        }

        public Dictionary<string, object> AsDictionary()
        {
            return Attributes;
        }

        public T Patch(T model)
        {
            Attributes.ToList().ForEach(attribute =>
                model.GetType().GetProperties().Where(x => x.Name.ToLower().Equals(attribute.Key.ToLower())).SingleOrDefault()
                    .SetValue(model, attribute.Value));
            return model;
        }

        public T Put(T model)
        {
            model.GetType().GetProperties().ToList().ForEach(property => {
                object value;

                this.Attributes.TryGetValue(property.Name, out value);

                object propertyValue = value != null ? Convert.ChangeType(value, property.PropertyType) : null;

                property.SetValue(model, propertyValue);
            });
            return model;
        }

        public void Set(T model)
        {
            model.GetType().GetProperties().ToList().ForEach(property => Attributes.Add(property.Name.ToLower(), property.GetValue(model)));
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Attributes.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Attributes[binder.Name] = value;

            return true;
        }

        internal void Bind(ModelBindingContext bindingContext)
        {
            if (bindingContext.BindingSource.Id.Equals(BindingSource.Body.Id))
            {
                TrackFromBody(bindingContext);
            }
            else if (bindingContext.BindingSource.Id.Equals(BindingSource.Form.Id))
            {
                TrackFromForm(bindingContext);
            }
            else if (bindingContext.BindingSource.Id.Equals(BindingSource.Query.Id))
            {
                TrackFromQuery(bindingContext);
            }
            else
            {
                TrackFromForm(bindingContext);
            }
        }

        private void TrackFromQuery(ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }

        private void TrackFromForm(ModelBindingContext bindingContext)
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            bindingContext.HttpContext.Request.Form.ToList().ForEach(x =>
            {
                var property = properties.Where(p => p.Name.ToUpper().Equals(x.Key.ToUpper())).SingleOrDefault();

                if (property != null)
                {
                    Attributes[property.Name] = SetValue(property.PropertyType, x.Value);
                }
            });
        }

        private void TrackFromBody(ModelBindingContext bindingContext)
        {
            string content = string.Empty;
            using (StreamReader reader = new StreamReader(bindingContext.HttpContext.Request.Body, Encoding.UTF8))
            {
                Task<string> task = reader.ReadToEndAsync();
                if (task.IsCompletedSuccessfully)
                {
                    content = task.Result;
                }
            }

            JObject jObject = JObject.Parse(content);

            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            foreach (var property in properties)
            {
                if (jObject[property.Name] != null)
                {
                    Attributes[property.Name] = SetValue(property.PropertyType, (string)jObject[property.Name]);
                }
            }
        }

        private object SetValue(Type type, StringValues values)
        {
            if (type.GetConstructors().ToList().Exists(constructor => constructor.GetParameters().Count() == 0))
            {
                object o = Activator.CreateInstance(type);

                return o;
            }
            if (type.IsGenericType && (
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                || type.GetGenericTypeDefinition() == typeof(ICollection<>)
                || type.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                foreach (var item in values.ToList())
                {
                }
            }
            else
            {
                return Convert.ChangeType(values.ToList()[0], type);
            }

            return null;
        }
    }
}
