using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelWrapper.Extensions.Patch
{
    public static class PatchExtensions
    {
        public static TModel Patch<TModel>(this IWrapRequest<TModel> request, TModel model) where TModel : class
        {
            var properties = typeof(TModel).GetProperties().Where(x => request.SuppliedProperties().Any(y => y == x.Name)).ToList();

            properties = properties.Where(p => !request.SuppressedProperties().Any(x => x.Equals(p.Name))).ToList();
            properties = properties.Where(p => !request.KeyProperties().Any(x => x.Equals(p.Name))).ToList();

            properties.ForEach(property => property.SetValue(model, property.GetValue(request.Model)));

            request.SetModelOnRequest(model, properties);

            return model;
        }
    }
}
