using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandResponse : ApplicationResponse<Order>
    {
        public DeleteOrderCommandResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
