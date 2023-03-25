using ModelWrapper.Interfaces;
using System.Linq;

namespace ModelWrapper.Extensions.Patch
{
    /// <summary>
    /// Class that extends patch functionality into ModelWrapper
    /// </summary>
    public static class PatchExtensions
    {
        /// <summary>
        /// Mathod that extends IWrapRequest<T> allowing use patch
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <param name="model">Entity were data from request will be patched</param>
        /// <returns>Entity updated</returns>
        public static TModel Patch<TModel>(
            this WrapRequest<TModel> request,
            TModel model
        ) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().Where(x => request.SuppliedProperties().Any(y => y.ToLower().Equals(x.Name.ToLower()))).ToList();

            properties = properties.Where(p => !request.IsPropertySuppressed(p.Name)).ToList();
            properties = properties.Where(p => !request.KeyProperties().Any(x => x.ToLower().Equals(p.Name.ToLower()))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(request.Model)));

            request.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
