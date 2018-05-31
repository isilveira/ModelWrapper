using System.Collections.Generic;
using System.Linq;
using ModelWrapper.Core;

namespace ModelWrapper
{
    public class Wrap<T> : IWrap<T> where T : class
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
    }
}
