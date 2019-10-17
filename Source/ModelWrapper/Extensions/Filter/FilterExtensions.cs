using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Filter
{
    public static class FilterExtensions
    {
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
