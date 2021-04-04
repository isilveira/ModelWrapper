using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : ApplicationRequest<Customer, GetCustomersByFilterQueryResponse>
    {
        public GetCustomersByFilterQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
