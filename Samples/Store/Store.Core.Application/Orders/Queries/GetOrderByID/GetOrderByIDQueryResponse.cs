using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQueryResponse : WrapResponse<Order>
    {
        public GetOrderByIDQueryResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
