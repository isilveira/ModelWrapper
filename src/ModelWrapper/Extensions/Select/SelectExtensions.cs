using ModelWrapper.Helpers;
using ModelWrapper.Models;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Select
{
	/// <summary>
	/// Class that extends select functionality into ModelWrapper
	/// </summary>
	public static class SelectExtensions
    {
        internal static Func<bool, bool, bool, bool> IsToLoadComplexPropertyWhenNotRequested = (isComplexProperty, loadComplexObjectByDefault, isRootObject) => isComplexProperty ? loadComplexObjectByDefault ? isRootObject : false : true;

        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get select properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static List<SelectedProperty> ResponseProperties<TModel>(
            this WrapRequest<TModel> source
        ) where TModel : class
        {
            List<string> requestProperties = new List<string>();

            source.AllProperties.Where(x =>
                x.Name.ToLower().Equals(Constants.CONST_RESPONSE_PROPERTIES.ToLower())
            ).ToList().ForEach(x =>
                requestProperties.Add(x.Value.ToString())
            );

            List<SelectedProperty> properties = GetValidProperties(source, typeof(TModel), requestProperties);

            source.RequestObject.SetValue(Constants.CONST_RESPONSE_PROPERTIES, properties.Select(x => string.Join(".", x.RequestedPropertyName.Split(".").Select(part => part.ToCamelCase()))));

            return properties;
        }
        private static List<SelectedProperty> GetValidProperties<TModel>(
            WrapRequest<TModel> source,
            Type type,
            List<string> requestProperties,
            string rootName = null,
            bool isFromCollection = false)
            where TModel : class
        {
            var properties = new List<SelectedProperty>();

            type.GetProperties()
                .Where(property =>
                    !source.IsPropertySuppressedResponse(string.Join(".", TermsHelper.GetTerms(rootName, ".").Union(new List<string> { property.Name }).Where(term => !string.IsNullOrWhiteSpace(term))))
                    && (
                        (requestProperties.Count == 0 && IsToLoadComplexPropertyWhenNotRequested(TypesHelper.TypeIsComplex(property.PropertyType), ConfigurationService.GetConfiguration().ByDefaultLoadComplexProperties, string.IsNullOrWhiteSpace(rootName)))
                        || (requestProperties.Any(requested => TermsHelper.GetTerms(requested, ".").FirstOrDefault().ToLower().Equals(property.Name.ToLower())))
                    )
                )
                .ToList()
                .ForEach(property =>
                {
                    if (!TypesHelper.TypeIsComplex(property.PropertyType))
                    {
                        properties.Add(new SelectedProperty
                        {
                            RequestedPropertyName = string.Join(".", TermsHelper.GetTerms(rootName, ".").Union(new List<string> { property.Name }).Where(term => !string.IsNullOrWhiteSpace(term))),
                            PropertyName = property.Name,
                            RootPropertyName = rootName,
                            PropertyType = property.PropertyType,
                            PropertyInfo = property
                        });
                    }
                    else if (ConfigurationService.GetConfiguration().ByDefaultLoadComplexProperties)
                    {
                        properties.AddRange(
                            GetValidProperties(
                                source,
                                TypesHelper.TypeIsCollection(property.PropertyType) ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType,
                                requestProperties
                                    .Where(requested => TermsHelper.GetTerms(requested, ".").FirstOrDefault().ToLower().Equals(property.Name.ToLower()))
                                    .Select(requested => string.Join(".", TermsHelper.GetTerms(requested, ".").Skip(1)))
                                    .Where(requested => !string.IsNullOrWhiteSpace(requested))
                                    .ToList(),
                                string.Join(".", TermsHelper.GetTerms(rootName, ".").Union(new List<string> { property.Name }).Where(term => !string.IsNullOrWhiteSpace(term))),
                                TypesHelper.TypeIsCollection(property.PropertyType)
                            )
                        );
                    }
                });

            return properties;
        }

        internal static SelectedModel GetSelectedModel<TModel>(
            this WrapRequest<TModel> source
        ) where TModel : class
        {
            var responseProperties = source.ResponseProperties().Select(property => property.TypedClone()).ToList();

            var selectedModel = new SelectedModel
            {
                Name = source.Model.GetType().Name,
                OriginalType = source.Model.GetType(),
                Properties = GetSelectedModelProperties(source.Model.GetType(), responseProperties)
            };

            return selectedModel;
        }

        private static List<SelectedModel> GetSelectedModelProperties(Type originalRootType, List<SelectedProperty> responseProperties)
        {
            var models = new List<SelectedModel>();

            responseProperties
                .Where(x => !x.RequestedPropertyName.Contains("."))
                .ToList()
                .ForEach(responseProperty =>
                    models.Add(new SelectedModel
                    {
                        Name = responseProperty.PropertyName,
                        RequestedName = responseProperty.RequestedPropertyName,
                        OriginalType = responseProperty.PropertyType,
                        OriginalPropertyInfo = responseProperty.PropertyInfo
                    }));

            responseProperties
                .Where(x => x.RequestedPropertyName.Contains("."))
                .Select(property =>
                {
                    property.RootPropertyName = property.RequestedPropertyName.Split(".")[0];

                    return property;
                })
                .ToList()
                .GroupBy(x => x.RootPropertyName)
                .ToList()
                .ForEach(x =>
                {
                    var property = x.FirstOrDefault();
                    var originalRootPropertyInfo = (TypesHelper.TypeIsCollection(originalRootType) ? originalRootType.GetGenericArguments()[0] : originalRootType).GetProperty(property.RootPropertyName);
                    var originalPropertyType = originalRootPropertyInfo.PropertyType;
                    models.Add(new SelectedModel
                    {
                        Name = property.RootPropertyName,
                        RequestedName = property.RootPropertyName,
                        OriginalType = originalPropertyType,
                        OriginalPropertyInfo = originalRootPropertyInfo,
                        Properties = GetSelectedModelProperties(originalPropertyType, x.ToList().Select(item =>
                        {
                            item.RootPropertyName = property.RootPropertyName;
                            item.RequestedPropertyName = item.RequestedPropertyName.Replace($"{property.RootPropertyName}.", "");
                            return item;
                        }).ToList())
                    });
                });

            return models;
        }

        /// <summary>
        /// Method that extends IQueryable<T> allowing to select results with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for select</returns>
        public static IQueryable<object> Select<TSource>(
            this IQueryable<TSource> source,
            WrapRequest<TSource> request
        ) where TSource : class
        {
            return source.Select(LambdasHelper.GenerateSelectExpression<TSource>(request.GetSelectedModel()));
		}
		public static object Select<TSource, TResult>(
            this TSource source,
			WrapRequest<TSource> request
		) 
            where TSource : class
            where TResult : class
		{
            return (new List<TSource>() { source }).AsQueryable().Select(request).SingleOrDefault();
		}
	}
}
