using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryResponse : WrapResponse<Order>
    {
        public GetOrdersByFilterQueryResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
