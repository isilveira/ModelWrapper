using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandResponse : WrapResponse<OrderedProduct>
    {
        public DeleteOrderedProductCommandResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
