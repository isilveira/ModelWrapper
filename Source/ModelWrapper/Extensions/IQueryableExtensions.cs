using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<object> FullSearch<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request, ref int resultCount) where TSource : class
        {
            return source.Filter(request).Search(request).Count(ref resultCount).OrderBy(request).Scope(request).Select(request);
        }
        public static IQueryable<object> FullSearch<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request, ref long resultCount) where TSource : class
        {
            return source.Filter(request).Search(request).Count(ref resultCount).OrderBy(request).Scope(request).Select(request);
        }
        public static IQueryable<object> Select<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            return source.Select(LambdaHelper.GenerateSelectExpression<TSource>(request.ResponseProperties()));
        }
        public static IQueryable<TSource> Filter<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            var filterProperties = request.FilterProperties();
            if (filterProperties == null || filterProperties.Count == 0)
            {
                return source;
            }

            request.RequestObject.Add(Constants.CONST_FILTER_PROPERTIES, filterProperties);

            var criteriaExp = LambdaHelper.GenerateFilterCriteriaExpression<TSource>(filterProperties);

            return source.Where(criteriaExp);
        }
        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
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

            //var criteriaExp = LambdaHelper.GenerateSearchCriteriaExpression(queryTokens, request);

            //return source.Where(criteriaExp);
            return source;
        }
        public static IQueryable<TSource> Count<TSource>(this IQueryable<TSource> source, ref int count) where TSource : class
        {
            count = source.Count();
            return source;
        }
        public static IQueryable<TSource> Count<TSource>(this IQueryable<TSource> source, ref long count) where TSource : class
        {
            count = source.Count();
            return source;
        }
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            var ordinationProperties = request.OrdinationProperties();

            string order = ordinationProperties.GetValue(Constants.CONST_ORDENATION_ORDER);
            string orderBy = ordinationProperties.GetValue(Constants.CONST_ORDENATION_ORDERBY);

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Type type = typeof(TSource);
            Expression lambda = ExpressionHelper.GenereteLambdaExpression<TSource>(ref type, orderBy);
            MethodInfo orderMethod;

            if (order.ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower()))
            {
                orderMethod = ReflectionHelper.GetMethodFromType(typeof(Queryable), "OrderBy", 2, 2);
            }
            else
            {
                orderMethod = ReflectionHelper.GetMethodFromType(typeof(Queryable), "OrderByDescending", 2, 2);
            }

            return (IQueryable<TSource>)orderMethod.MakeGenericMethod(typeof(TSource), type).Invoke(null, new object[] { source, lambda });
        }
        public static IQueryable<TModel> Scope<TModel>(this IQueryable<TModel> source, IWrapRequest<TModel> request) where TModel : class
        {
            var paginationProperties = request.PaginationProperties();

            int pageNumber = paginationProperties.GetValue(Constants.CONST_PAGINATION_NUMBER);
            int pageSize = paginationProperties.GetValue(Constants.CONST_PAGINATION_SIZE);

            return source.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}
