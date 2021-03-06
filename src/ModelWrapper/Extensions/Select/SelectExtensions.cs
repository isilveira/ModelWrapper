﻿using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
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
        internal static List<PropertyInfo> ResponseProperties<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
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

            source.RequestObject.SetValue(Constants.CONST_RESPONSE_PROPERTIES, properties.Select(x => x.Name.ToCamelCase()));

            return properties;
        }

        public static List<PropertyInfo> ResponseProperties<TModel>(
            this WrapResponse<TModel> source
        ) where TModel : class
        {
            return source.OriginalRequest.ResponseProperties();
        }

        public static bool HasResponseProperty<TModel>(
            this WrapResponse<TModel> source,
            string name
        ) where TModel : class
        {
            return source.ResponseProperties().Any(property => property.Name.ToLower().Equals(name.ToLower()));
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
            return source.Select(LambdaHelper.GenerateSelectExpression<TSource>(request.ResponseProperties()));
        }

        public static object  Select<TSource>(this TSource source, IWrapRequest<TSource> request) where TSource : class
        {
            var newType = ReflectionHelper.CreateNewType(request.ResponseProperties());

            var newInstance = Activator.CreateInstance(newType);

            ReflectionHelper.Copy(source, newInstance);

            return newInstance;
        }
    }
}
