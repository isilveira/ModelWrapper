﻿using ModelWrapper.Binders;
using ModelWrapper.Helpers;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Filter
{
	public class FilterProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    /// <summary>
    /// Class that extends filter functionality into ModelWrapper
    /// </summary>
    public static class FilterExtensions
    {
        public static void ClearFilters<TModel>(this WrapRequest<TModel> source)
            where TModel : class
        {
            typeof(TModel)
                .GetProperties()
                .Where(property => !source.IsPropertySuppressed(property.Name))
                .SelectMany(property => CriteriasHelper.GetPropertyTypeCriteria(property.PropertyType).Select(criteria => $"{property.Name.ToCamelCase()}{criteria}"))
                .ToList()
                .ForEach(propertyCriteria =>
                {
                    source.AllProperties.RemoveAll(property => property.Name.ToLower().Equals(propertyCriteria.ToLower()));
                });
		}
		public static void AddFilter<TModel>(this WrapRequest<TModel> source, string filterProperty, object filterValue)
			where TModel : class
		{
			var memberBinder = new WrapRequestMemberBinder(filterProperty, WrapPropertySource.FromQuery, true);
			source.GetType().GetMethod("TrySetMember").Invoke(source, new object[] { memberBinder, filterValue.ToString() });
		}

		/// <summary>
		/// Method that extends IWrapRequest<T> allowing to get filter properties from request
		/// </summary>
		/// <typeparam name="TModel">Generic type of the entity</typeparam>
		/// <param name="source">Self IWrapRequest<T> instance</param>
		/// <returns>Returns a dictionary with properties and values found</returns>
		internal static List<FilterProperty> FilterProperties<TModel>(
            this WrapRequest<TModel> source
        ) where TModel : class
        {
            var filterProperties = new List<FilterProperty>();

            foreach (var property in typeof(TModel).GetProperties().Where(x =>
                 !source.IsPropertySuppressed(x.Name)
            ).ToList())
            {
                foreach (var criteria in CriteriasHelper.GetPropertyTypeCriteria(property.PropertyType))
                {
                    var criteriaName = $"{property.Name.ToCamelCase()}{criteria}";
                    var listObjects = new List<object>();
                    foreach (var value in source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(criteriaName.ToLower())
                    ).Select(x => x.Value).ToList())
                    {
                        bool changed = false;
                        object typedValue = TypesHelper.TryChangeType(value.ToString(), property.PropertyType, out changed);
                        if (changed)
                        {
                            listObjects.Add(typedValue);
                        }
                    }
                    if (listObjects.Count > 0)
                    {
                        filterProperties.Add(new FilterProperty { Name = criteriaName, Value = listObjects.Count > 1 ? listObjects : listObjects.FirstOrDefault() });
                    }
                }
            }

            return filterProperties;
        }

        public static List<FilterProperty> Filters<TModel>(
            this WrapResponse<TModel> source
        ) where TModel : class
        {
            return source.OriginalRequest.FilterProperties();
        }

        public static object GetFilterProperty<TModel>(
            this WrapResponse<TModel> source, string name
        ) where TModel : class
        {
            var filterProperty = source.Filters().SingleOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

            if (filterProperty == null)
            {
                return string.Empty;
            }
            else
            {
                return filterProperty.Value;
            }
        }

        /// <summary>
        /// Method that extends IQueryable<T> allowing to filter query with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for filter</returns>
        public static IQueryable<TSource> Filter<TSource>(
            this IQueryable<TSource> source,
            WrapRequest<TSource> request
        ) where TSource : class
        {
            var filterProperties = request.FilterProperties();
            if (filterProperties == null || filterProperties.Count == 0)
            {
                return source;
            }

            request.RequestObject.SetValue(Constants.CONST_FILTER_PROPERTIES.ToCamelCase(), filterProperties);

            var filterDictionary = new Dictionary<string, object>();

            filterProperties.ForEach(filter => filterDictionary.Add(filter.Name, filter.Value));

            var criteriaExp = LambdasHelper.GenerateFilterCriteriaExpression<TSource>(filterDictionary);

            return source.Where(criteriaExp);
        }
    }
}
