using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryResponse : WrapResponse<Product>
    {
        public GetProductByIDQueryResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
