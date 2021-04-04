using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandResponse : ApplicationResponse<Order>
    {
        public PatchOrderCommandResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
