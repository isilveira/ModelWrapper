using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelWrapper.Extensions.Post
{
    public static class PostExtensions
    {
        public static TModel Post<TModel>(
            this IWrapRequest<TModel> request
        ) where TModel : class
        {
            var model = Activator.CreateInstance<TModel>();

            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !request.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !request.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(request.Model)));

            request.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
