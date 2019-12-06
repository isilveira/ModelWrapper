using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Extensions.GetModel
{
    public static class GetModelExtensions
    {
        public static TModel GetModel<TModel>(this IWrapResponse<TModel> source)
            where TModel : class
        {
            if (source.Data is ICollection<object>)
            {
                throw new Exception("Data is a collection!");
            }

            var model = Activator.CreateInstance<TModel>();

            ReflectionHelper.Copy(source.Data, model);

            return model;
        }
    }
}
