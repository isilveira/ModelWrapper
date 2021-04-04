using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Products.Queries.GetProductByID
{
    public class GetProductByIDQuery : ApplicationRequest<Product, GetProductByIDQueryResponse>
    {
        public GetProductByIDQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.OrderedProducts);

            ConfigSuppressedResponseProperties(x => x.Images);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
