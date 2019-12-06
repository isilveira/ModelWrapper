using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandResponse : ResponseBase<Order>
    {
        public DeleteOrderCommandResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
