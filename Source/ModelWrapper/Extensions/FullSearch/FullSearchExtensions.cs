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
    public static class FullSearchExtensions
    {
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
