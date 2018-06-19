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
            model.GetType().GetProperties().ToList().ForEach(property =>
            {
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
            if (bindingContext.BindingSource.Id.Equals(BindingSource.Body.Id) && bindingContext.HttpContext.Request.ContentLength > 0)
            {
                TrackFromBody(bindingContext);
                return;
            }

            if (bindingContext.BindingSource.Id.Equals(BindingSource.Form.Id) && bindingContext.HttpContext.Request.HasFormContentType)
            {
                TrackFromForm(bindingContext);
                return;
            }

            if (bindingContext.BindingSource.Id.Equals(BindingSource.Query.Id) && bindingContext.HttpContext.Request.Query.Count > 0)
            {
                TrackFromQuery(bindingContext);
                return;
            }

            if (bindingContext.BindingSource.Id.Equals(BindingSource.Custom.Id))
            {
                if (bindingContext.HttpContext.Request.HasFormContentType)
                {
                    TrackFromForm(bindingContext);
                    return;
                }

                if (bindingContext.HttpContext.Request.ContentLength > 0)
                {
                    TrackFromBody(bindingContext);
                    return;
                }

                if (bindingContext.HttpContext.Request.Query.Count > 0)
                {
                    TrackFromQuery(bindingContext);
                    return;
                }
            }
        }

        private void TrackFromQuery(ModelBindingContext bindingContext)
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            if (bindingContext.HttpContext.Request.Query.Count > 0)
            {
                bindingContext.HttpContext.Request.Query.ToList().ForEach(query =>
                   {
                       var property = properties.Where(p => p.Name.ToLower().Equals(query.Key.ToLower())).SingleOrDefault();
                       if (property != null)
                       {
                           Attributes[property.Name] = SetValue(property.PropertyType, query.Value);
                       }
                   });
            }
        }

        private void TrackFromForm(ModelBindingContext bindingContext)
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            if (bindingContext.HttpContext.Request.HasFormContentType)
            {
                bindingContext.HttpContext.Request.Form.ToList().ForEach(x =>
                {
                    var property = properties.Where(p => p.Name.ToUpper().Equals(x.Key.ToUpper())).SingleOrDefault();

                    if (property != null)
                    {
                        Attributes[property.Name] = SetValue(property.PropertyType, x.Value);
                    }
                });
            }
        }

        private void TrackFromBody(ModelBindingContext bindingContext)
        {
            string content = string.Empty;
            if (bindingContext.HttpContext.Request.Body != null)
            {
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
                    if (jObject.Properties().ToList().Exists(jproperty => jproperty.Name.ToLower().Equals(property.Name.ToLower())))
                    {
                        Attributes[property.Name] = SetValue(property.PropertyType, jObject[jObject.Properties().ToList().Where(jproperty => jproperty.Name.ToLower().Equals(property.Name.ToLower())).SingleOrDefault().Name]);
                    }
                }
            }
        }
        //REFATORAR: Tratar os tipos de dados possiveis e para casos de JSON realizar a navegação dentro dos filhos preenchendo a model.
        // usar recursividade para o preenchimento em árvore.
        private object SetValue(Type type, object value)
        {
            object o = null;

            if (type.GetConstructors().ToList().Exists(constructor => constructor.GetParameters().Count() == 0))
            {
                o = Activator.CreateInstance(type);
            }
            if (type.IsGenericType && o != null && (o is IList || o is IEnumerable || o is ICollection))
            {
                if (value is JToken && ((JToken)value).Children().Count() > 0)
                {
                    //Carrega os jtokens filhos para a lista de objetos
                }
            }
            else
            {
                if (value is JToken)
                {
                    return Convert.ChangeType(value, type);
                }
                else
                {
                    return Convert.ChangeType(((StringValues)value).ToList()[0], type);
                }
            }

            return o;
        }
    }
}
