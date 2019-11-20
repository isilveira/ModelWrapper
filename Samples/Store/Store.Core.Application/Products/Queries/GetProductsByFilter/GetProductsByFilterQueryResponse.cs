using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQueryResponse : WrapResponse<Product>
    {
        public GetProductsByFilterQueryResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
