using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQueryResponse : ResponseBase<Product>
    {
        public GetProductsByFilterQueryResponse(WrapRequest<Product> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
