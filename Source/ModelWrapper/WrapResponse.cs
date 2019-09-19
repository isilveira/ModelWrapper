using System;
using System.Collections.Generic;
using System.Dynamic;
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
            Add(nameof(data), data);
        }
    }
}
