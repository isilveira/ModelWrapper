using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Commands.PutProduct
{
    public class PutProductCommandResponse : ResponseBase<Product>
    {
        public PutProductCommandResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
