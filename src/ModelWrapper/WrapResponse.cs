using ModelWrapper.Extensions;
using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    /// <summary>
    /// Class that wrap the response
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class WrapResponse<TModel>: IWrapResponse<TModel>
        where TModel : class
    {
        internal IWrapRequest<TModel> OriginalRequest{ get; set; }
        public long ResultCount { get; set; }
        public string Message { get; set; }
        public object Request { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// Class empty constructor
        /// </summary>
        public WrapResponse() { }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="request">WrapRequest object</param>
        /// <param name="data">Response data</param>
        /// <param name="message">Response message</param>
        /// <param name="resultCount">Count of data returned</param>
        public WrapResponse(
            WrapRequest<TModel> request,
            object data,
            string message = "Successful operation!",
            long? resultCount = null
        )
        {
            OriginalRequest = request;
            Message = message;
            ResultCount = resultCount.HasValue ? resultCount.Value : data is ICollection<object> ? ((ICollection<object>)data).Count : 1;
            Request = request.RequestObject;
            Data = data;
        }

        public TModel GetModel()
        {
            if (Data is ICollection<object>)
            {
                throw new Exception("Data is a collection!");
            }

            var model = Activator.CreateInstance<TModel>();

            ReflectionHelper.Copy(Data, model);

            return model;
        }

        public IList<TModel> GetModels()
        {
            if (!(Data is ICollection<object>))
            {
                throw new Exception("Data is not a collection!");
            }

            var models = new List<TModel>();

            foreach(var data in ((ICollection<object>)Data))
            {
                var model = Activator.CreateInstance<TModel>();

                ReflectionHelper.Copy(data, model);

                models.Add(model);
            }

            return models;
        }
    }
}
