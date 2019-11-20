using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandResponse : WrapResponse<OrderedProduct>
    {
        public PatchOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
