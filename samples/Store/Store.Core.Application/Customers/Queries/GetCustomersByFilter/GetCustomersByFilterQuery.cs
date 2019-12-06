using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : RequestBase<Customer, GetCustomersByFilterQueryResponse>
    {
        public GetCustomersByFilterQuery()
        {
            ConfigKeys(x => x.CustomerID);

            ConfigSuppressedProperties(x => x.Orders);

            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
