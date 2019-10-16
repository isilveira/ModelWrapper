using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions.Ordination
{
    public static class OrdinationExtensions
    {
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
            var lambda = ExpressionHelper.GenereteLambdaExpression<TSource>(ref type, orderBy);
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
    }
}
