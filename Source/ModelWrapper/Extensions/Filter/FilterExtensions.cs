using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Linq;

namespace ModelWrapper.Extensions.Filter
{
    public static class FilterExtensions
    {
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
