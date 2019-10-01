using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions
{
    public static class WrapRequestExtensions
    {
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

            properties = properties.Where(p => !source.SupressedProperties.Any(x => x.Name.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties.Any(x => x.Name.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }
        public static TModel Put<TModel>(this IWrapRequest<TModel> source, TModel model) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !source.SupressedProperties.Any(x => x.Name.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties.Any(x => x.Name.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }
        public static TModel Patch<TModel>(this IWrapRequest<TModel> source, TModel model) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().Where(x => source.SuppliedProperties.Any(y => y.Name == x.Name)).ToList();

            properties = properties.Where(p => !source.SupressedProperties.Any(x => x.Name.Equals(p.Name))).ToList();
            properties = properties.Where(p => !source.KeyProperties.Any(x => x.Name.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(source.Model)));

            source.SetModelOnRequest(model, properties);

            return model;
        }

        public static IQueryable<object> Select<TModel>(this IQueryable<TModel> source, WrapRequest<TModel> request) where TModel: class
        {
            return source.Select(ExpressionsHelper.SelectExpression<TModel>(request.ResponseProperties));
        }
    }
}
