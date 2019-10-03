using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQueryResponse : WrapResponse<OrderedProduct>
    {
        public GetOrderedProductsByFilterQueryResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
