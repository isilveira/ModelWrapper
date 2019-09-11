using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ModelWrapper
{
    public class WrapResponse<TModel> : Dictionary<string, object>
        where TModel : class
    {
        //public string Message { get; set; }
        //public long ResultCount { get; set; }
        //public Dictionary<string, object> Request { get; set; }
        //public Dictionary<string, object> Data { get; set; }
        public WrapResponse() { }
        public WrapResponse(WrapRequest<TModel> request, TModel data, string message = null, long? resultCount = null)
        {
            Initialize();
            if (!string.IsNullOrWhiteSpace(message))
            {
                //Message = message;
                Add(nameof(message), message);
            }
            if (resultCount.HasValue)
            {
                //ResultCount = resultCount.Value;
                Add(nameof(resultCount), resultCount);
            }
            //Request = request.GetRequestAsDictionary();
            Add(nameof(request), request.GetRequestAsDictionary());
            Add(nameof(data), data);
        }

        private void Initialize()
        {
            //Request = new Dictionary<string, object>();
        }
    }
}
