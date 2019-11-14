using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions
{
    /// <summary>
    /// Class that extends functionalities of the IWrapRequest instances
    /// </summary>
    public static class IWrapRequestExtensions
    {
        /// <summary>
        /// Method that returns a list of configured keys properties.
        /// </summary>
        /// <typeparam name="TModel">Type of the model/entity class</typeparam>
        /// <param name="source">IWrapRequest object instance</param>
        /// <returns>List of keys properties</returns>
        internal static List<string> KeyProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_KEYS);
        }
        /// <summary>
        /// Method that returns a list of configured suppressed properties.
        /// </summary>
        /// <typeparam name="TModel">Type of the model/entity class</typeparam>
        /// <param name="source">IWrapRequest object instance</param>
        /// <returns>List of supperssed properties</returns>
        internal static List<string> SuppressedProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPRESSED);
        }
        /// <summary>
        /// Method that returns a list of configured suppressed response properties.
        /// </summary>
        /// <typeparam name="TModel">Type of the model/entity class</typeparam>
        /// <param name="source">IWrapRequest object instance</param>
        /// <returns>List of supperssed response properties</returns>
        internal static List<string> SuppressedResponseProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPRESSED_RESPONSE);
        }
        /// <summary>
        /// Method that returns a list of configured supplied properties.
        /// </summary>
        /// <typeparam name="TModel">Type of the model/entity class</typeparam>
        /// <param name="source">IWrapRequest object instance</param>
        /// <returns>List of spupplied properties</returns>
        internal static List<string> SuppliedProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPLIED);
        }
        /// <summary>
        /// Method that set model properties into the request object
        /// </summary>
        /// <typeparam name="TModel">Type of the model/entity class</typeparam>
        /// <param name="source">IWrapRequest object instance</param>
        /// <param name="model">Model object</param>
        /// <param name="properties">List of properties that will be inserted on the request object</param>
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
