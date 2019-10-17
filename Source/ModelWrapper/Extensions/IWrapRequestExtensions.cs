using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions
{
    public static class IWrapRequestExtensions
    {
        internal static List<string> KeyProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_KEYS);
        }
        internal static List<string> SuppressedProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPRESSED);
        }
        internal static List<string> SuppressedResponseProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPRESSED_RESPONSE);
        }
        internal static List<string> SuppliedProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPLIED);
        }
        internal static void SetModelOnRequest<TModel>(
            this IWrapRequest<TModel> source,
            TModel model,
            IList<PropertyInfo> properties
        ) where TModel : class
        {
            if (properties.Count > 0)
            {
                var dictionary = new Dictionary<string, object>();

                properties.ToList().ForEach(property =>
                {
                    dictionary.Add(property.Name, property.GetValue(model));
                });

                source.RequestObject.Add(Constants.CONST_MODEL, dictionary);
            }
        }
    }
}
