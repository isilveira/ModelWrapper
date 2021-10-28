using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Models;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions.Select
{
    /// <summary>
    /// Class that extends select functionality into ModelWrapper
    /// </summary>
    public static class SelectExtensions
    {
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get select properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static List<SelectedProperty> ResponseProperties<TModel>(
            this IWrapRequest<TModel> source
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

        private static List<SelectedProperty> GetValidProperties<TModel>(IWrapRequest<TModel> source, Type type, List<string> requestProperties, string rootName = null, bool isFromCollection = false) where TModel : class
        {
            var suppressedResponseProperties = source.SuppressedResponseProperties();

            var properties = new List<SelectedProperty>();

            foreach (var property in type.GetProperties())
            {
                if (requestProperties.Count == 0 && !suppressedResponseProperties.Any(x => x.ToLower().Equals(property.Name.ToLower())))
                {
                    properties.Add(new SelectedProperty { RequestedPropertyName = property.Name, PropertyName = property.Name, RootPropertyName = rootName, IsFromInnerObject = !string.IsNullOrWhiteSpace(rootName), IsFromCollectionObject = isFromCollection, PropertyType = property.PropertyType, PropertyInfo = property });
                }
                else
                {
                    var notSuppressedResponseProperties = requestProperties.Where(x => !suppressedResponseProperties.Any(y => y.ToLower().Equals(x.ToLower()))).ToList();
                    foreach (var validProperty in notSuppressedResponseProperties)
                    {
                        var validPropertyParts = validProperty.Split(".");
                        if (property.Name.ToLower().Equals(validPropertyParts[0].ToLower()))
                        {
                            var rootPropertyName = (string.IsNullOrWhiteSpace(rootName) ? "" : (rootName + ".")) + property.Name;
                            if (validPropertyParts.Count() > 1 && property.PropertyType.IsClass)
                            {
                                properties.AddRange(GetValidProperties(source, property.PropertyType, new List<string> { string.Join(".", validPropertyParts.ToList().Skip(1)) }, rootPropertyName));
                            }
                            else if (validPropertyParts.Count() > 1 && property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                            {
                                properties.AddRange(GetValidProperties(source, property.PropertyType.GetGenericArguments()[0], new List<string> { string.Join(".", validPropertyParts.ToList().Skip(1)) }, rootPropertyName, true));
                            }
                            else
                            {
                                properties.Add(new SelectedProperty { RequestedPropertyName = rootPropertyName, PropertyName = property.Name, RootPropertyName = rootName, IsFromInnerObject = !string.IsNullOrWhiteSpace(rootName), IsFromCollectionObject = isFromCollection, PropertyType = property.PropertyType, PropertyInfo = property });
                            }
                        }
                    }
                }
            }

            return properties;
        }

        internal static SelectedModel GetSelectedModel<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            var responseProperties = source.ResponseProperties();

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
                .Where(x => !x.IsFromInnerObject)
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
                .Where(x => x.IsFromInnerObject)
                .ToList()
                .GroupBy(x => x.RootPropertyName)
                .ToList()
                .ForEach(x =>
                {
                    var property = x.FirstOrDefault();
                    var originalRootPropertyInfo = originalRootType.GetProperty(property.RootPropertyName);
                    models.Add(new SelectedModel
                    {
                        Name = property.RootPropertyName,
                        RequestedName = property.RootPropertyName,
                        OriginalType = property.IsFromCollectionObject ? originalRootPropertyInfo.PropertyType.GetGenericArguments()[0] : originalRootPropertyInfo.PropertyType,
                        OriginalPropertyInfo = originalRootPropertyInfo,
                        IsCollection = property.IsFromCollectionObject,
                        Properties = x
                            .ToList()
                            .Select(y => new SelectedModel { Name = y.PropertyName, RequestedName = y.RequestedPropertyName, OriginalType = y.PropertyType, OriginalPropertyInfo = y.PropertyInfo })
                            .ToList()
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
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            return source.Select(LambdaHelper.GenerateSelectExpression<TSource>(request.GetSelectedModel()));
        }
    }
}
