using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
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
            return source.Select(LambdaHelper.GenerateSelectExpression(request));
        }
        public static IQueryable<TSource> Filter<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            //if (request.FilterProperties == null || request.FilterProperties.Count == 0)
            //{
            //    return source;
            //}

            //var criteriaExp = LambdaHelper.GenerateFilterCriteriaExpression(request);

            //return source.Where(criteriaExp);
            return source;
        }
        public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            //if (string.IsNullOrWhiteSpace(request.Query))
            //    return source;

            //var queryTokens = TokenHelper.GetTokens(request.Query, request.QueryPhrase);

            //request.Query = string.Join("+", queryTokens.ToArray());

            //if (queryTokens.Count == 0)
            //    return source;

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
            //if (source == null)
            //{
            //    throw new ArgumentNullException("source");
            //}

            //if (request == null)
            //{
            //    throw new ArgumentNullException(nameof(request));
            //}

            //if (string.IsNullOrWhiteSpace(request.OrderBy))
            //{
            //    var someProperty = typeof(TSource).GetProperties().FirstOrDefault();
            //    if (someProperty == null)
            //    {
            //        throw new ArgumentNullException(nameof(request.OrderBy));
            //    }
            //    request.OrderBy = someProperty.Name;
            //}

            //Type type = typeof(TSource);
            //Expression lambda = ExpressionHelper.GenereteLambdaExpression<TSource>(ref type, request.OrderBy);
            //MethodInfo orderMethod;

            //if (request.Order == Order.ASCENDING)
            //{
            //    orderMethod = ReflectionHelper.GetMethodFromType(typeof(Queryable), "OrderBy", 2, 2);
            //}
            //else
            //{
            //    orderMethod = ReflectionHelper.GetMethodFromType(typeof(Queryable), "OrderByDescending", 2, 2);
            //}

            //return (IQueryable<TSource>)orderMethod.MakeGenericMethod(typeof(TSource), type).Invoke(null, new object[] { source, lambda });
            return source;
        }
        public static IQueryable<TModel> Scope<TModel>(this IQueryable<TModel> source, IWrapRequest<TModel> request) where TModel : class
        {
            return source;//.Skip(request.PageNumber * request.PageSize).Take(request.PageSize);
        }
    }
}
