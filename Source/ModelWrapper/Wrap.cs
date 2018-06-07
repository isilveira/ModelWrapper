using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelWrapper.Core;
using ModelWrapper.Core.Binders.Models;

namespace ModelWrapper
{
    [ModelBinder(BinderType = typeof(WrapModelBinder))]
    public class Wrap<T> : DynamicObject, IWrap<T>
        where T : class
    {
        private Dictionary<string, object> Attributes;

        static Wrap() { }
        public Wrap() { }
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
            throw new System.NotImplementedException();
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

        }
    }
}
