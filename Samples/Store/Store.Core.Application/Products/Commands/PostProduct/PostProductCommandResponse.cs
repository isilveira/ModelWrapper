using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommandResponse : WrapResponse<Product>
    {
        public PostProductCommandResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
