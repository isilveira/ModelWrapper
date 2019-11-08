using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandResponse : WrapResponse<Product>
    {
        public DeleteProductCommandResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
