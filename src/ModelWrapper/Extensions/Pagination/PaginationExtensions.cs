using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Pagination
{
    /// <summary>
    /// Class that extends pagination functionality into ModelWrapper
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get DefaultReturnedCollectionSize property
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns the default return collection size</returns>
        internal static int DefaultReturnedCollectionSize<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            object size = null;
            source.ConfigValues.TryGetValue(Constants.CONST_DEFAULT_COLLECTION_SIZE, out size);

            return size == null ? ConfigurationService.GetConfiguration().DefaultReturnedCollectionSize : Convert.ToInt32(size);
        }
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get MaximumReturnedCollectionSize property
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns the maximum return collection size</returns>
        internal static int MaximumReturnedCollectionSize<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            object configMax = null;
            source.ConfigValues.TryGetValue(Constants.CONST_MAX_COLLECTION_SIZE, out configMax);

            return configMax == null ? ConfigurationService.GetConfiguration().MaximumReturnedCollectionSize : Convert.ToInt32(configMax);
        }
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get MinimumReturnedCollectionSize property
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns the minimum return collection size</returns>
        internal static int MinimumReturnedCollectionSize<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            object configMin = null;
            source.ConfigValues.TryGetValue(Constants.CONST_MIN_COLLECTION_SIZE, out configMin);

            return configMin == null ? ConfigurationService.GetConfiguration().MinimumReturnedCollectionSize : Convert.ToInt32(configMin);
        }
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get pagination properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static Dictionary<string, int> PaginationProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            Dictionary<string, int> paginationProperties = new Dictionary<string, int>();

            #region GET PAGE SIZE VALUE
            var pageSizeProperty = source.AllProperties.Where(x =>
                x.Name.ToLower().Equals(Constants.CONST_PAGINATION_SIZE.ToLower())
                && x.Source == WrapPropertySource.FromQuery
            ).FirstOrDefault();
            if (pageSizeProperty != null)
            {
                bool changed = false;
                int typedValue = CriteriaHelper.TryChangeType<int>(pageSizeProperty.Value.ToString(), out changed);
                if (changed)
                {
                    typedValue = typedValue > source.MaximumReturnedCollectionSize() ? source.MaximumReturnedCollectionSize() : typedValue;
                    typedValue = typedValue < source.MinimumReturnedCollectionSize() ? source.MinimumReturnedCollectionSize() : typedValue;
                    paginationProperties.Add(Constants.CONST_PAGINATION_SIZE, typedValue);
                }
            }
            else
            {
                paginationProperties.Add(Constants.CONST_PAGINATION_SIZE, source.DefaultReturnedCollectionSize());
            }
            #endregion

            #region GET PAGE NUMBER VALUE
            var pageNumberProperty = source.AllProperties.Where(x => x.Name.ToLower().Equals(Constants.CONST_PAGINATION_NUMBER.ToLower()) && x.Source == WrapPropertySource.FromQuery).FirstOrDefault();
            if (pageNumberProperty != null)
            {
                bool changed = false;
                int typedValue = CriteriaHelper.TryChangeType<int>(pageNumberProperty.Value.ToString(), out changed);
                if (changed)
                {
                    paginationProperties.Add(Constants.CONST_PAGINATION_NUMBER, typedValue != default(int) ? typedValue : 0);
                }
            }
            else
            {
                paginationProperties.Add(Constants.CONST_PAGINATION_NUMBER, 0);
            }
            #endregion

            source.RequestObject.Add(Constants.CONST_PAGINATION, paginationProperties);

            return paginationProperties;
        }
        /// <summary>
        /// Method that extends IQueryable<T> allowing to paginate query with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for pagination</returns>
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
