using ModelWrapper.Interfaces;
using System;
using System.Linq;

namespace ModelWrapper.Extensions.Post
{
    /// <summary>
    /// Class that extends post functionality into ModelWrapper
    /// </summary>
    public static class PostExtensions
    {
        /// <summary>
        /// Mathod that extends IWrapRequest<T> allowing use post
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>New entity</returns>
        public static TModel Post<TModel>(
            this IWrapRequest<TModel> request
        ) where TModel : class
        {
            var model = Activator.CreateInstance<TModel>();

            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !request.IsPropertySuppressed(p.Name)).ToList();
            properties = properties.Where(p => !request.KeyProperties().Any(x => x.ToLower().Equals(p.Name.ToLower()))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(request.Model)));

            request.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
