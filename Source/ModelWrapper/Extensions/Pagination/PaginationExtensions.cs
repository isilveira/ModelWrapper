using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Linq;

namespace ModelWrapper.Extensions.Pagination
{
    public static class PaginationExtensions
    {
        public static IQueryable<TSource> Page<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            var paginationProperties = request.PaginationProperties();

            int pageNumber = paginationProperties.GetValue(Constants.CONST_PAGINATION_NUMBER);
            int pageSize = paginationProperties.GetValue(Constants.CONST_PAGINATION_SIZE);

            return source.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}
