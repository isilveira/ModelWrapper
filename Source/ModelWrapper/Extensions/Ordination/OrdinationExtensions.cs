using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions.Ordination
{
    public static class OrdinationExtensions
    {
        internal static Dictionary<string, string> OrdinationProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            var ordinationProperties = new Dictionary<string, string>();

            #region GET ORDER PROPERTY
            var orderProperty = source.AllProperties.Where(x =>
                    x.Name.ToLower().Equals(Constants.CONST_ORDENATION_ORDER.ToLower())
                    && x.Source == WrapPropertySource.FromQuery
                ).FirstOrDefault();
            if (orderProperty != null
                && (orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower())
                || orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_DESCENDING.ToLower())))
            {
                var value = orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower()) ? Constants.CONST_ORDENATION_ORDER_ASCENDING : Constants.CONST_ORDENATION_ORDER_DESCENDING;
                ordinationProperties.Add(Constants.CONST_ORDENATION_ORDER, value);
            }
            else
            {
                ordinationProperties.Add(Constants.CONST_ORDENATION_ORDER, Constants.CONST_ORDENATION_ORDER_ASCENDING);
            }
            #endregion

            #region GET ORDERBY PROPERTY
            var orderByProperty = source.AllProperties.Where(x =>
                    x.Name.ToLower().Equals(Constants.CONST_ORDENATION_ORDERBY.ToLower())
                    && x.Source == WrapPropertySource.FromQuery
                ).FirstOrDefault();
            if (orderByProperty != null && typeof(TModel).GetProperties().Any(x => x.Name.ToLower().Equals(orderByProperty.Value.ToString().ToLower())))
            {
                var property = typeof(TModel).GetProperties().Where(x => x.Name.ToLower().Equals(orderByProperty.Value.ToString().ToLower())).SingleOrDefault();
                ordinationProperties.Add(Constants.CONST_ORDENATION_ORDERBY, property.Name.ToCamelCase());
            }
            else
            {
                if (source.KeyProperties().Any())
                {
                    ordinationProperties.Add(Constants.CONST_ORDENATION_ORDERBY, source.KeyProperties().FirstOrDefault());
                }
            }
            #endregion

            source.RequestObject.Add(Constants.CONST_ORDENATION, ordinationProperties);

            return ordinationProperties;
        }
        public static IQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
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
