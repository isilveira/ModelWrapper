using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions.Ordination
{
    public class Ordination
    {
        public string OrderBy { get; set; }
        public string Order { get; set; }
    }

    /// <summary>
    /// Class that extends ordination functionality into ModelWrapper
    /// </summary>
    public static class OrdinationExtensions
    {
        public static void ClearOrdination<TModel>(this WrapRequest<TModel> source)
            where TModel : class
        {
            source.AllProperties.RemoveAll(property => property.Name.ToLower().Equals(Constants.CONST_ORDENATION_ORDER.ToLower()) || property.Name.ToLower().Equals(Constants.CONST_ORDENATION_ORDERBY.ToLower()));
        }
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get ordination properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static Ordination Ordination<TModel>(
            this WrapRequest<TModel> source
        ) where TModel : class
        {
            var ordination = new Ordination();

            #region GET ORDER PROPERTY
            var orderProperty = source.AllProperties.Where(x =>
                    x.Name.ToLower().Equals(Constants.CONST_ORDENATION_ORDER.ToLower())
                    && x.Source == WrapPropertySource.FromQuery
                ).FirstOrDefault();
            if (orderProperty != null
                && (orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower())
                || orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_DESCENDING.ToLower())))
            {
                ordination.Order = orderProperty.Value.ToString().ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower()) ? Constants.CONST_ORDENATION_ORDER_ASCENDING : Constants.CONST_ORDENATION_ORDER_DESCENDING;
            }
            else
            {
                ordination.Order = Constants.CONST_ORDENATION_ORDER_ASCENDING;
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
                ordination.OrderBy = property.Name.ToCamelCase();
            }
            else
            {
                if (source.KeyProperties().Any())
                {
                    ordination.OrderBy = source.KeyProperties().FirstOrDefault();
                }
            }
            #endregion

            source.RequestObject.SetValue(Constants.CONST_ORDENATION, ordination);

            return ordination;
        }

        public static Ordination Ordination<TModel>(
            this WrapResponse<TModel> source
        ) where TModel : class
        {
            return source.OriginalRequest.Ordination();
        }

        /// <summary>
        /// Method that extends IQueryable<T> allowing to order query with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for ordination</returns>
        public static IQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> source,
            WrapRequest<TSource> request
        ) where TSource : class
        {
            var ordination = request.Ordination();

            string order = ordination.Order;
            string orderBy = ordination.OrderBy;

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Type type = typeof(TSource);
            var lambda = LambdasHelper.GenereteLambdaExpression<TSource>(ref type, orderBy);
            MethodInfo orderMethod;

            if (order.ToLower().Equals(Constants.CONST_ORDENATION_ORDER_ASCENDING.ToLower()))
            {
                orderMethod = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "OrderBy", 2, 2);
            }
            else
            {
                orderMethod = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "OrderByDescending", 2, 2);
            }

            return (IQueryable<TSource>)orderMethod.MakeGenericMethod(typeof(TSource), type).Invoke(null, new object[] { source, lambda });
        }
    }
}
