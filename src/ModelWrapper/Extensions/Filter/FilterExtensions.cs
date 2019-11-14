using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Filter
{
    /// <summary>
    /// Class that extends filter functionality into ModelWrapper
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get filter properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static Dictionary<string, object> FilterProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            Dictionary<string, object> filterProperties = new Dictionary<string, object>();

            foreach (var property in typeof(TModel).GetProperties().Where(x =>
                 !source.SuppressedProperties().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).ToList())
            {
                foreach (var criteria in CriteriaHelper.GetPropertyTypeCriteria(property.PropertyType))
                {
                    var criteriaName = $"{property.Name.ToCamelCase()}{criteria}";
                    var listObjects = new List<object>();
                    foreach (var value in source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(criteriaName.ToLower())
                    ).Select(x => x.Value).ToList())
                    {
                        bool changed = false;
                        object typedValue = CriteriaHelper.TryChangeType(value.ToString(), property.PropertyType, out changed);
                        if (changed)
                        {
                            listObjects.Add(typedValue);
                        }
                    }
                    if (listObjects.Count > 0)
                    {
                        filterProperties.Add(criteriaName, listObjects.Count > 1 ? listObjects : listObjects.FirstOrDefault());
                    }
                }
            }

            return filterProperties;
        }
        /// <summary>
        /// Method that extends IQueryable<T> allowing to filter query with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for filter</returns>
        public static IQueryable<TSource> Filter<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            var filterProperties = request.FilterProperties();
            if (filterProperties == null || filterProperties.Count == 0)
            {
                return source;
            }

            request.RequestObject.Add(Constants.CONST_FILTER_PROPERTIES.ToCamelCase(), filterProperties);

            var criteriaExp = LambdaHelper.GenerateFilterCriteriaExpression<TSource>(filterProperties);

            return source.Where(criteriaExp);
        }
    }
}
