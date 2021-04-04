using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQueryResponse : ApplicationResponse<Order>
    {
        public GetOrderByIDQueryResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
