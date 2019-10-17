using ModelWrapper.Interfaces;
using System.Linq;

namespace ModelWrapper.Extensions.Put
{
    public static class PutExtensions
    {
        public static TModel Put<TModel>(
            this IWrapRequest<TModel> request,
            TModel model
        ) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().ToList();

            properties = properties.Where(p => !request.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !request.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(request.Model)));

            request.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
