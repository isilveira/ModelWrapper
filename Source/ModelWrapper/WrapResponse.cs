using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ModelWrapper
{
    public class WrapResponse<TModel> : Dictionary<string, object>
        where TModel : class
    {
        public WrapResponse() { }
        public WrapResponse(WrapRequest<TModel> request, TModel data, string message = null, long? resultCount = null)
        {
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
        }
        public WrapResponse(WrapRequest<TModel> request, IList<TModel> data, string message = null, long? resultCount = null)
        {
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
        }

        private IList<Dictionary<string, object>> ResponseProperties(WrapRequest<TModel> request, IList<TModel> data)
        {
            IList<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();

            foreach(var item in data)
            {
                dictionaries.Add(ResponseProperties(request, item));
            }

            return dictionaries;
        }

        private Dictionary<string, object> ResponseProperties(WrapRequest<TModel> request, TModel data)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach(var property in typeof(TModel).GetProperties().Where(p=>
                !request.SupressedProperties.Any(x=>x.Name.Equals(p.Name))
            ).ToList())
            {
                if (request.ResponseProperties.Count > 0)
                {
                    var responseProperty = request.ResponseProperties.SingleOrDefault(rp => rp.Name.Equals(property.Name));
                    if (responseProperty != null)
                    {
                        dictionary.Add(responseProperty.Name, responseProperty.GetValue(data));
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
