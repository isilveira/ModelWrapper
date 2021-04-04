using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.PatchProduct
{
    public class PatchProductCommandResponse : ApplicationResponse<Product>
    {
        public PatchProductCommandResponse(WrapRequest<Product> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
