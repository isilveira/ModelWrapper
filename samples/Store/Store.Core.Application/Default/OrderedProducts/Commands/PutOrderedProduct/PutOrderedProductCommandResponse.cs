using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommandResponse : ApplicationResponse<OrderedProduct>
    {
        public PutOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
