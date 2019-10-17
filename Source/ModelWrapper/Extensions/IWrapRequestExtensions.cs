using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper.Extensions
{
    public static class IWrapRequestExtensions
    {
        internal static List<string> KeyProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_KEYS);
        }
        internal static List<string> SuppressedProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPRESSED);
        }
        internal static List<string> SuppressedResponseProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPRESSED_RESPONSE);
        }
        internal static List<string> SuppliedProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.ConfigProperties.GetValue(Constants.CONST_SUPPLIED);
        }
        internal static List<PropertyInfo> ResponseProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            List<string> requestProperties = new List<string>();

            source.AllProperties.Where(x =>
                x.Name.ToLower().Equals(Constants.CONST_RESPONSE_PROPERTIES.ToLower())
            ).ToList().ForEach(x =>
                requestProperties.Add(x.Value.ToString())
            );

            List<PropertyInfo> properties = typeof(TModel).GetProperties().Where(x =>
                (requestProperties.Count == 0 || requestProperties.Any(y => y.ToLower().Equals(x.Name.ToLower())))
                && !source.SuppressedResponseProperties().ToList().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).ToList();

            source.RequestObject.Add(Constants.CONST_RESPONSE_PROPERTIES, properties.Select(x => x.Name.ToCamelCase()));

            return properties;
        }
        internal static Dictionary<string, object> FilterProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            Dictionary<string, object> filterProperties = new Dictionary<string, object>();

            foreach (var property in typeof(TModel).GetProperties().Where(x =>
                 !source.SuppressedProperties().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).ToList())
            {
                foreach (var criteria in CriteriaHelper.GetPropertyTypeCriteria(property.PropertyType))
                {
                    var criteriaName = $"{property.Name.ToCamelCase()}{criteria}";
                    var listObjects = new List<object>();
                    foreach (var value in source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(criteriaName.ToLower())
                    ).Select(x => x.Value).ToList())
                    {
                        bool changed = false;
                        object typedValue = CriteriaHelper.TryChangeType(value.ToString(), property.PropertyType, ref changed);
                        if (changed)
                        {
                            listObjects.Add(typedValue);
                        }
                    }
                    if (listObjects.Count > 0)
                    {
                        filterProperties.Add(criteriaName, listObjects.Count > 1 ? listObjects : listObjects.FirstOrDefault());
                    }
                }
            }

            return filterProperties;
        }
        internal static int DefaultReturnedCollectionSize<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            object size = null;
            source.ConfigValues.TryGetValue(Constants.CONST_DEFAULT_COLLECTION_SIZE, out size);

            return size == null ? Configuration.GetConfiguration().DefaultReturnedCollectionSize : Convert.ToInt32(size);
        }
        internal static int MaxReturnedCollectionSize<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            object configMax = null;
            source.ConfigValues.TryGetValue(Constants.CONST_MAX_COLLECTION_SIZE, out configMax);

            return configMax == null ? Configuration.GetConfiguration().MaximumReturnedCollectionSize: Convert.ToInt32(configMax);
        }
        internal static int MinReturnedCollectionSize<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            object configMin = null;
            source.ConfigValues.TryGetValue(Constants.CONST_MIN_COLLECTION_SIZE, out configMin);

            return configMin == null ? Configuration.GetConfiguration().MinimumReturnedCollectionSize : Convert.ToInt32(configMin);
        }
        internal static Dictionary<string, int> PaginationProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
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
                int typedValue = CriteriaHelper.TryChangeType<int>(pageSizeProperty.Value.ToString(), ref changed);
                if (changed)
                {
                    typedValue = typedValue > source.MaxReturnedCollectionSize() ? source.MaxReturnedCollectionSize() : typedValue;
                    typedValue = typedValue < source.MinReturnedCollectionSize() ? source.MinReturnedCollectionSize() : typedValue;
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
                int typedValue = CriteriaHelper.TryChangeType<int>(pageNumberProperty.Value.ToString(), ref changed);
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
        internal static Dictionary<string, string> OrdinationProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
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
        internal static Dictionary<string, object> QueryProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            var queryProperties = new Dictionary<string, object>();

            #region GET QUERY PROPERTY
            var queryProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryProperty != null)
            {
                bool changed = false;
                string typedValue = CriteriaHelper.TryChangeType<string>(queryProperty.Value.ToString(), ref changed);
                if (changed)
                {
                    queryProperties.Add(Constants.CONST_QUERY, typedValue);
                }
                else
                {
                    queryProperties.Add(Constants.CONST_QUERY, string.Empty);
                }
            }
            else
            {
                queryProperties.Add(Constants.CONST_QUERY, string.Empty);
            } 
            #endregion

            #region GET QUERY STRICT PROPERTY
            var queryStrictProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY_STRICT.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryStrictProperty != null)
            {
                bool changed = false;
                bool typedValue = CriteriaHelper.TryChangeType<bool>(queryStrictProperty.Value.ToString(), ref changed);
                if (changed)
                {
                    queryProperties.Add(Constants.CONST_QUERY_STRICT, typedValue);
                }
                else
                {
                    queryProperties.Add(Constants.CONST_QUERY_STRICT, false);
                }
            }
            else
            {
                queryProperties.Add(Constants.CONST_QUERY_STRICT, false);
            } 
            #endregion

            #region GET QUERY PHRASE PROPERTY
            var queryPhraseProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY_PHRASE.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryPhraseProperty != null)
            {
                bool changed = false;
                bool typedValue = CriteriaHelper.TryChangeType<bool>(queryPhraseProperty.Value.ToString(), ref changed);
                if (changed)
                {
                    queryProperties.Add(Constants.CONST_QUERY_PHRASE, typedValue);
                }
                else
                {
                    queryProperties.Add(Constants.CONST_QUERY_PHRASE, false);
                }
            }
            else
            {
                queryProperties.Add(Constants.CONST_QUERY_PHRASE, false);
            } 
            #endregion

            source.RequestObject.Add(Constants.CONST_QUERY_PROPERTIES, queryProperties);

            return queryProperties;
        }
        internal static void SetModelOnRequest<TModel>(this IWrapRequest<TModel> source, TModel model, IList<PropertyInfo> properties) where TModel : class
        {
            if (properties.Count > 0)
            {
                var dictionary = new Dictionary<string, object>();

                properties.ToList().ForEach(property =>
                {
                    dictionary.Add(property.Name, property.GetValue(model));
                });

                source.RequestObject.Add(Constants.CONST_MODEL, dictionary);
            }
        }
    }
}
