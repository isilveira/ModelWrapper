using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandResponse : ApplicationResponse<OrderedProduct>
    {
        public DeleteOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
