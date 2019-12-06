using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandResponse : ResponseBase<Order>
    {
        public PatchOrderCommandResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
