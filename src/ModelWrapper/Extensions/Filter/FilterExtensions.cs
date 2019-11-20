using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Filter
{
    public class FilterProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

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
        internal static List<FilterProperty> FilterProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            var filterProperties = new List<FilterProperty>();

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
                        filterProperties.Add(new FilterProperty { Name = criteriaName, Value = listObjects.Count > 1 ? listObjects : listObjects.FirstOrDefault() });
                    }
                }
            }

            return filterProperties;
        }

        public static List<FilterProperty> Filters<TModel>(
            this WrapResponse<TModel> source
        ) where TModel : class
        {
            return source.OriginalRequest.FilterProperties();
        }

        public static object GetFilterProperty<TModel>(
            this WrapResponse<TModel> source, string name
        ) where TModel : class
        {
            var filterProperty = source.Filters().SingleOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

            if (filterProperty == null)
            {
                return string.Empty;
            }
            else
            {
                return filterProperty.Value;
            }
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

            request.RequestObject.SetValue(Constants.CONST_FILTER_PROPERTIES.ToCamelCase(), filterProperties);

            var filterDictionary = new Dictionary<string, object>();

            filterProperties.ForEach(filter => filterDictionary.Add(filter.Name, filter.Value));

            var criteriaExp = LambdaHelper.GenerateFilterCriteriaExpression<TSource>(filterDictionary);

            return source.Where(criteriaExp);
        }
    }
}
