using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryResponse : ApplicationResponse<Product>
    {
        public GetProductByIDQueryResponse(WrapRequest<Product> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
