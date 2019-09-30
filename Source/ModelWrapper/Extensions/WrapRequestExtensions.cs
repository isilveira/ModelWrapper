using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions
{
    public static class WrapRequestExtensions
    {
        internal static void SetModelOnRequest<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            if (source.SuppliedProperties.Count > 0)
            {
                var dictionary = new Dictionary<string, object>();

                source.SuppliedProperties.ToList().ForEach(property => {
                    var propertyValue = source.AllProperties.Where(ap => ap.Name.ToLower().Equals(property.Name.ToLower())).SingleOrDefault();

                    if (propertyValue != null)
                    {
                        dictionary.Add(property.Name, propertyValue.Value); 
                    }
                });

                source.RequestObject.Add("Model", dictionary);
            }
        }
        public static TModel Post<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            if (source.SuppliedProperties.Count>0)
            {
                source.SetModelOnRequest();
                return source.Model;
            }

            return null;
        }
    }
}
