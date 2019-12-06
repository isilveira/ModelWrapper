using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryResponse : ResponseBase<Product>
    {
        public GetProductByIDQueryResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
