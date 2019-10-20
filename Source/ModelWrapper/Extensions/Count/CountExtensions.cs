using System.Linq;

namespace ModelWrapper.Extensions.Count
{
    /// <summary>
    /// Class that extends count functionality into ModelWrapper
    /// </summary>
    public static class CountExtensions
    {
        /// <summary>
        /// Method that extends IQuerable<T> add count functionality
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">IQueryable source</param>
        /// <param name="count">Count of entities</param>
        /// <returns>IQueryable<T></returns>
        public static IQueryable<TSource> Count<TSource>(
            this IQueryable<TSource> source,
            out int count
        ) where TSource : class
        {
            count = source.Count();

            return source;
        }
        /// <summary>
        /// Method that extends IQuerable<T> add count functionality
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">IQueryable source</param>
        /// <param name="count">Count of entities</param>
        /// <returns>IQueryable<T></returns>
        public static IQueryable<TSource> LongCount<TSource>(
            this IQueryable<TSource> source,
            out long count
        ) where TSource : class
        {
            count = source.LongCount();

            return source;
        }
    }
}
