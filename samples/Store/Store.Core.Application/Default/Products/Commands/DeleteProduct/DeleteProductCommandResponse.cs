using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandResponse : ApplicationResponse<Product>
    {
        public DeleteProductCommandResponse(WrapRequest<Product> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
