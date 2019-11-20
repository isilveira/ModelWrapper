using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQuery : WrapRequest<Customer>, IRequest<GetCustomerByIDQueryResponse>
    {
        public GetCustomerByIDQuery()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.Orders);
            ConfigSuppressedResponseProperties(x => x.Orders);
        }
    }
}
