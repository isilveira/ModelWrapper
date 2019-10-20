using ModelWrapper.Extensions.Count;
using ModelWrapper.Extensions.Filter;
using ModelWrapper.Extensions.Ordination;
using ModelWrapper.Extensions.Pagination;
using ModelWrapper.Extensions.Search;
using ModelWrapper.Extensions.Select;
using ModelWrapper.Interfaces;
using System.Linq;

namespace ModelWrapper.Extensions.FullSearch
{
    /// <summary>
    /// Class that extends full search functionality into ModelWrapper
    /// </summary>
    public static class FullSearchExtensions
    {
        /// <summary>
        /// Method that extends IQueryable allowing all search functionalities from ModelWrapper
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <param name="count">Count of entities</param>
        /// <returns>Returns IQueryable instance with with the configuration for a full search</returns>
        public static IQueryable<object> FullSearch<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request,
            out int count
        ) where TSource : class
        {
            return source
                .Filter(request)
                .Search(request)
                .Count(out count)
                .OrderBy(request)
                .Page(request)
                .Select(request);
        }
        /// <summary>
        /// Method that extends IQueryable allowing all search functionalities from ModelWrapper
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <param name="count">Count of entities</param>
        /// <returns>Returns IQueryable instance with with the configuration for a full search</returns>
        public static IQueryable<object> FullSearch<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request,
            out long count
        ) where TSource : class
        {
            return source
                .Filter(request)
                .Search(request)
                .LongCount(out count)
                .OrderBy(request)
                .Page(request)
                .Select(request);
        }
    }
}
