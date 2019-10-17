using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions.Select
{
    public static class SelectExtensions
    {
        internal static List<PropertyInfo> ResponseProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            List<string> requestProperties = new List<string>();

            source.AllProperties.Where(x =>
                x.Name.ToLower().Equals(Constants.CONST_RESPONSE_PROPERTIES.ToLower())
            ).ToList().ForEach(x =>
                requestProperties.Add(x.Value.ToString())
            );

            List<PropertyInfo> properties = typeof(TModel).GetProperties().Where(x =>
                (requestProperties.Count == 0 || requestProperties.Any(y => y.ToLower().Equals(x.Name.ToLower())))
                && !source.SuppressedResponseProperties().ToList().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).ToList();

            source.RequestObject.Add(Constants.CONST_RESPONSE_PROPERTIES, properties.Select(x => x.Name.ToCamelCase()));

            return properties;
        }
        public static IQueryable<object> Select<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            return source.Select(LambdaHelper.GenerateSelectExpression<TSource>(request.ResponseProperties()));
        }
    }
}
