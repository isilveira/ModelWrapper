using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System.Linq;

namespace ModelWrapper.Extensions.Select
{
    public static class SelectExtensions
    {
        public static IQueryable<object> Select<TSource>(this IQueryable<TSource> source, IWrapRequest<TSource> request) where TSource : class
        {
            return source.Select(LambdaHelper.GenerateSelectExpression<TSource>(request.ResponseProperties()));
        }
    }
}
