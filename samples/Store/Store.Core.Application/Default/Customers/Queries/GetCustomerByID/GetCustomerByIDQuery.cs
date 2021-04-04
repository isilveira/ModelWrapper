using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQuery : ApplicationRequest<Customer, GetCustomerByIDQueryResponse>
    {
        public GetCustomerByIDQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
