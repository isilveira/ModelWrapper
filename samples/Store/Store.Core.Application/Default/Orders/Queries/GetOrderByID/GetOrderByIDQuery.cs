using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQuery : ApplicationRequest<Order, GetOrderByIDQueryResponse>
    {
        public GetOrderByIDQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Customer);
            ConfigSuppressedProperties(x => x.OrderedProducts);

            ConfigSuppressedResponseProperties(x => x.Customer);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
        }
    }
}
