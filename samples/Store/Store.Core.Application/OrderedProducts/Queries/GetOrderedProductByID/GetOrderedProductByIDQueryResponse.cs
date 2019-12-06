using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQueryResponse : ResponseBase<OrderedProduct>
    {
        public GetOrderedProductByIDQueryResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
