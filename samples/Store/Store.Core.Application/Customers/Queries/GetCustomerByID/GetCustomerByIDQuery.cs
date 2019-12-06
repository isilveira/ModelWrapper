using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQuery : RequestBase<Customer, GetCustomerByIDQueryResponse>
    {
        public GetCustomerByIDQuery()
        {
            ConfigKeys(x => x.CustomerID);

            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
