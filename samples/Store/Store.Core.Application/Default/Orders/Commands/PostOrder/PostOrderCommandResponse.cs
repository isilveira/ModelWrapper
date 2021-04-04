using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Commands.PostOrder
{
    public class PostOrderCommandResponse : ApplicationResponse<Order>
    {
        public PostOrderCommandResponse(WrapRequest<Order> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
