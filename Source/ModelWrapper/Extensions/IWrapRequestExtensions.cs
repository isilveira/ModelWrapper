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

            source.RequestObject.Add(Constants.CONST_RESPONSE_PROPERTIES, properties.Select(x => x.Name));

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
                    var criteriaName = $"{property.Name}{criteria}";
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
                    paginationProperties.Add(Constants.CONST_PAGINATION_SIZE, typedValue != default(int) ? typedValue : Configuration.GetConfiguration().DefaultPageSize);
                }
            }
            else
            {
                paginationProperties.Add(Constants.CONST_PAGINATION_SIZE, Configuration.GetConfiguration().DefaultPageSize);
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
                    paginationProperties.Add(Constants.CONST_PAGINATION_NUMBER, typedValue != default(int) ? typedValue : Configuration.GetConfiguration().DefaultPageNumber);
                }
            }
            else
            {
                paginationProperties.Add(Constants.CONST_PAGINATION_NUMBER, Configuration.GetConfiguration().DefaultPageNumber);
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
                ordinationProperties.Add(Constants.CONST_ORDENATION_ORDER, orderProperty.Value.ToString());
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
                ordinationProperties.Add(Constants.CONST_ORDENATION_ORDERBY, property.Name);
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
        internal static void SetModelOnRequest<TModel>(this IWrapRequest<TModel> source, TModel model, IList<PropertyInfo> properties) where TModel : class
        {
            if (properties.Count > 0)
            {
                var dictionary = new Dictionary<string, object>();

                properties.ToList().ForEach(property =>
                {
                    dictionary.Add(property.Name, property.GetValue(model));
                });

                source.RequestObject.Add("Model", dictionary);
            }
        }
        public static TModel Post<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            var model = Activator.CreateInstance<TModel>();

            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !source.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }
        public static TModel Put<TModel>(this IWrapRequest<TModel> source, TModel model) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !source.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }
        public static TModel Patch<TModel>(this IWrapRequest<TModel> source, TModel model) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().Where(x => source.SuppliedProperties().Any(y => y == x.Name)).ToList();

            properties = properties.Where(p => !source.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
