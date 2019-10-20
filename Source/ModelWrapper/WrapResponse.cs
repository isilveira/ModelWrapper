using ModelWrapper.Extensions;
using ModelWrapper.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    /// <summary>
    /// Class that wrap the response
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class WrapResponse<TModel> : Dictionary<string, object>, IWrapResponse<TModel>
        where TModel : class
    {
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
            string message = null,
            long? resultCount = null
        )
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Add(nameof(message), message);
            }
            if (resultCount.HasValue)
            {
                Add(nameof(resultCount), resultCount);
            }
            Add(nameof(request), request.RequestObject);
            Add(nameof(data), ResponseProperties(request, data));
        }
        /// <summary>
        /// Method that handles the responsed data
        /// </summary>
        /// <param name="request">WrapRequest object</param>
        /// <param name="data">Response data</param>
        /// <returns>List of dictionaries where data is returned</returns>
        private IList<Dictionary<string, object>> ResponseProperties(
            WrapRequest<TModel> request,
            object data
        )
        {
            IList<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();

            if (data is ICollection<object>)
            {
                foreach (var item in (ICollection<object>)data)
                {
                    dictionaries.Add(ResponseObjectProperties(request, item));
                }
            }
            else
            {
                dictionaries.Add(ResponseObjectProperties(request, data));
            }

            return dictionaries;
        }
        /// <summary>
        /// Method that handles the responsed data
        /// </summary>
        /// <param name="request">WrapRequest object</param>
        /// <param name="data">Response data</param>
        /// <returns>Dictionary where data is returned</returns>
        private Dictionary<string, object> ResponseObjectProperties(
            IWrapRequest<TModel> request,
            object data
        )
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (var property in data.GetType().GetProperties().Where(p =>
                 !request.SuppressedResponseProperties().Any(x => x.Equals(p.Name))
            ).ToList())
            {
                dictionary.Add(property.Name.ToCamelCase(), property.GetValue(data));
            }

            return dictionary;
        }
    }
}
