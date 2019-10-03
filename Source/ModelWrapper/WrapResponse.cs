using ModelWrapper.Interfaces;
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
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"),"WrapResponse");

            if (!string.IsNullOrWhiteSpace(message))
            {
                Add(nameof(message), message);
            }
            if (resultCount.HasValue)
            {
                Add(nameof(resultCount), resultCount);
            }
            Add(nameof(request), request.GetRequestAsDictionary());
            Add(nameof(data), ResponseProperties(request, data));

            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyyMMdd-HH:mm:ss.fff"), "WrapResponse");
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

        private Dictionary<string, object> ResponseObjectProperties(WrapRequest<TModel> request, object data)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (var property in data.GetType().GetProperties().Where(p =>
                 !request.SupressedResponseProperties.Any(x => x.Name.Equals(p.Name))
            ).ToList())
            {
                if (request.ResponseProperties.Count > 0)
                {
                    var responseProperty = request.ResponseProperties.FirstOrDefault(rp => rp.Name.Equals(property.Name));
                    if (responseProperty != null)
                    {
                        dictionary.Add(responseProperty.Name, property.GetValue(data));
                    }
                }
                else
                {
                    dictionary.Add(property.Name, property.GetValue(data));
                }
            }

            return dictionary;
        }
    }
}
