using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQuery : WrapRequest<Customer>, IRequest<GetCustomersByFilterQueryResponse>
    {
        public GetCustomersByFilterQuery()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.Orders);
            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
