using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandResponse : ApplicationResponse<OrderedProduct>
    {
        public PatchOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
