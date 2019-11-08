using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommandResponse : WrapResponse<OrderedProduct>
    {
        public PostOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
