using ModelWrapper.Extensions;
using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    public class WrapResponse : IWrapResponse
    {
        public int StatusCode { get; set; }
        public int InternalCode { get; set; }
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
            int statusCode,
            int internalCode,
            object request,
            object data,
            string message = "Successful operation!",
            long? resultCount = null
        )
        {
            StatusCode = statusCode;
            InternalCode = internalCode;
            Message = message;
            ResultCount = resultCount.HasValue ? resultCount.Value : data is ICollection<object> ? ((ICollection<object>)data).Count : 1;
            Request = request;
            Data = data;
        }
    }
    /// <summary>
    /// Class that wrap the response
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class WrapResponse<TModel> : WrapResponse, IWrapResponse<TModel>
        where TModel : class
    {
        internal WrapRequest<TModel> OriginalRequest { get; set; }
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
        ) : base(200, 200, request.RequestObject, data, message, resultCount)
        {
            OriginalRequest = request;
        }
    }
}
