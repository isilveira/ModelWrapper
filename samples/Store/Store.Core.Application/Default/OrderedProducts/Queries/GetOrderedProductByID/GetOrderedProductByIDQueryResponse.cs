using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQueryResponse : ApplicationResponse<OrderedProduct>
    {
        public GetOrderedProductByIDQueryResponse(WrapRequest<OrderedProduct> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
