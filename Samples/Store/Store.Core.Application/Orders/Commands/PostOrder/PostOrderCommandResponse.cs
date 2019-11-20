using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommandResponse : WrapResponse<Order>
    {
        public PostOrderCommandResponse(WrapRequest<Order> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
