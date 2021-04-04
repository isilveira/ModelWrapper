using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommandResponse : ApplicationResponse<OrderedProduct>
    {
        public PostOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
