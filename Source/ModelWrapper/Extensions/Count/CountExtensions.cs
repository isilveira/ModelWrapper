using System.Linq;

namespace ModelWrapper.Extensions.Count
{
    public static class CountExtensions
    {
        public static IQueryable<TSource> Count<TSource>(this IQueryable<TSource> source, out int count) where TSource : class
        {
            count = source.Count();

            return source;
        }
        public static IQueryable<TSource> LongCount<TSource>(this IQueryable<TSource> source, out long count) where TSource : class
        {
            count = source.LongCount();

            return source;
        }
    }
}
