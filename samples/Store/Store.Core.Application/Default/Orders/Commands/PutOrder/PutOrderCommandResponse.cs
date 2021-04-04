using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Commands.PutOrder
{
    public class PutOrderCommandResponse : ApplicationResponse<Order>
    {
        public PutOrderCommandResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
