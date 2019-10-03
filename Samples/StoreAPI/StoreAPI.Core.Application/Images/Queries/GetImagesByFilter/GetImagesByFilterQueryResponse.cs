using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryResponse : WrapResponse<Image>
    {
        public GetImagesByFilterQueryResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
