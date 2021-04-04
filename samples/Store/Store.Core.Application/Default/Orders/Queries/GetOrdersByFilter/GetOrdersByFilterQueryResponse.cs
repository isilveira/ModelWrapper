using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryResponse : ApplicationResponse<Order>
    {
        public GetOrdersByFilterQueryResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
