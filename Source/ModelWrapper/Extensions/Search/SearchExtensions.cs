using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Search
{
    public static class SearchExtensions
    {
        public static IQueryable<TSource> Search<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            var queryProperties = request.QueryProperties();

            var query = queryProperties.GetValue(Constants.CONST_QUERY).ToString();
            var queryStrict = (bool)queryProperties.GetValue(Constants.CONST_QUERY_STRICT);
            var queryPhrase = (bool)queryProperties.GetValue(Constants.CONST_QUERY_PHRASE);
            if (string.IsNullOrWhiteSpace(query))
                return source;

            var queryTokens = TokenHelper.GetTokens(query, queryPhrase);

            query = string.Join("+", queryTokens.ToArray());

            if (queryTokens.Count == 0)
                return source;

            List<string> searchableProperties = new List<string>();

            searchableProperties = typeof(TSource).GetProperties().Where(x =>
                 !request.SuppressedProperties().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).Select(x => x.Name).ToList();

            var criteriaExp = LambdaHelper.GenerateSearchCriteriaExpression<TSource>(searchableProperties, queryTokens, queryStrict);

            return source.Where(criteriaExp);
        }
    }
}
