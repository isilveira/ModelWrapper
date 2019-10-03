using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQueryResponse : WrapResponse<OrderedProduct>
    {
        public GetOrderedProductByIDQueryResponse(WrapRequest<OrderedProduct> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
