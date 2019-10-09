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
        internal static List<string> GetConfigProperties<TModel>(this IWrapRequest<TModel> source, string name) where TModel : class
        {
            var configProperties = source.ConfigProperties.Where(x => x.Name == name).SingleOrDefault();
            return configProperties != null ? configProperties.Properties : new List<string>();
        }
        internal static List<string> KeyProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.GetConfigProperties(Constants.CONST_KEYS);
        }
        internal static List<string> SuppressedProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.GetConfigProperties(Constants.CONST_SUPRESSED);
        }
        internal static List<string> SuppressedResponseProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.GetConfigProperties(Constants.CONST_SUPPRESSED_RESPONSE);
        }
        internal static List<string> SuppliedProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.GetConfigProperties(Constants.CONST_SUPPLIED);
        }
        internal static List<string> ResponseProperties<TModel>(this IWrapRequest<TModel> source) where TModel : class
        {
            return source.GetConfigProperties("Response");
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
