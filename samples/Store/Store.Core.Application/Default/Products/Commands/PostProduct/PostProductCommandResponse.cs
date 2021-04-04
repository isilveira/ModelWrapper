using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Commands.PostProduct
{
    public class PostProductCommandResponse : ApplicationResponse<Product>
    {
        public PostProductCommandResponse(WrapRequest<Product> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
