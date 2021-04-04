using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.PutProduct
{
    public class PutProductCommandResponse : ApplicationResponse<Product>
    {
        public PutProductCommandResponse(WrapRequest<Product> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
