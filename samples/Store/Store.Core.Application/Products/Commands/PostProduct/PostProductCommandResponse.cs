using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.PostProduct
{
    public class PostProductCommandResponse : ResponseBase<Product>
    {
        public PostProductCommandResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
