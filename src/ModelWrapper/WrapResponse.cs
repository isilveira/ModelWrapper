using ModelWrapper.Interfaces;
using System.Collections.Generic;

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
        public Dictionary<string, object> Notifications { get; set; }
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
            Dictionary<string, object> notifications = null,
            string message = "Successful operation!",
            long? resultCount = null
        )
        {
            StatusCode = statusCode;
            InternalCode = internalCode;
            Message = message;
            ResultCount = resultCount.HasValue ? resultCount.Value : data is ICollection<object> ? ((ICollection<object>)data).Count : data is null ? 0 : 1;
            Request = request;
            Data = data;
            Notifications = notifications;
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
            Dictionary<string, object> notifications = null,
            string message = "Successful operation!",
            long? resultCount = null
        ) : base(200, 200, request.RequestObject, data, notifications, message, resultCount)
        {
            OriginalRequest = request;
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="statusCode">Http status code</param>
        /// <param name="internalCode">Internal status code</param>
        /// <param name="request">WrapRequest object</param>
        /// <param name="data">Response data</param>
        /// <param name="exception">Response exception</param>
        /// <param name="message">Response message</param>
        /// <param name="resultCount">Count of data returned</param>
        public WrapResponse(
            int statusCode,
            int internalCode,
            WrapRequest<TModel> request,
            object data,
            Dictionary<string, object> notifications = null,
            string message = "Successful operation!",
            long? resultCount = null
        ) : base(statusCode, internalCode, request.RequestObject, data, notifications, message, resultCount)
        {
            OriginalRequest = request;
        }
    }
}
