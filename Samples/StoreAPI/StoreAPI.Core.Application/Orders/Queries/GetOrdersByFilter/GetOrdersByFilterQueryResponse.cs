using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryResponse : WrapResponse<Order>
    {
        public GetOrdersByFilterQueryResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
