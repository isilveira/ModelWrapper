using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Extensions.GetModels
{
    public static class GetModelsExtensions
    {

        public static IList<TModel> GetModels<TModel>(this IWrapResponse<TModel> source)
            where TModel : class
        {
            if (!(source.Data is ICollection<object>))
            {
                throw new Exception("Data is not a collection!");
            }

            var models = new List<TModel>();

            foreach (var data in ((ICollection<object>)source.Data))
            {
                var model = Activator.CreateInstance<TModel>();

                ReflectionsHelper.Copy(data, model);

                models.Add(model);
            }

            return models;
        }
    }
}
