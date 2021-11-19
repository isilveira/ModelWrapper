using Store.Core.Domain.Entities.Default;
using System.Linq;

namespace Store.Core.Application.Default.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQuery : ApplicationRequest<Product, GetProductsByFilterQueryResponse>
    {
        public GetProductsByFilterQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties("Images");
            ConfigSuppressedProperties("OrderedProducts");

            ConfigSuppressedResponseProperties("Category");
            ConfigSuppressedResponseProperties("Images.Id");
            ConfigSuppressedResponseProperties("Images");
            ConfigSuppressedResponseProperties("OrderedProducts.Order.Customer.RegisteredAt");
        }
    }
}
