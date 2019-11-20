using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandResponse : WrapResponse<OrderedProduct>
    {
        public DeleteOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
