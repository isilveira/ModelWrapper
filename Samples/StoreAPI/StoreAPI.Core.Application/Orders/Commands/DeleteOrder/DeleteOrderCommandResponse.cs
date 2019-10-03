using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandResponse : WrapResponse<Order>
    {
        public DeleteOrderCommandResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
