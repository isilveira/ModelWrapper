using ModelWrapper.Extensions;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    public class WrapResponse<TModel> : Dictionary<string, object>, IWrapResponse<TModel>
        where TModel : class
    {
        public WrapResponse() { }
        public WrapResponse(WrapRequest<TModel> request, object data, string message = null, long? resultCount = null)
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

        private IList<Dictionary<string, object>> ResponseProperties(WrapRequest<TModel> request, object data)
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

        private Dictionary<string, object> ResponseObjectProperties(IWrapRequest<TModel> request, object data)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (var property in data.GetType().GetProperties().Where(p =>
                 !request.SuppressedResponseProperties().Any(x => x.Equals(p.Name))
            ).ToList())
            {
                dictionary.Add(property.Name, property.GetValue(data));
            }

            return dictionary;
        }
    }
}
