using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryResponse : ResponseBase<Order>
    {
        public GetOrdersByFilterQueryResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
