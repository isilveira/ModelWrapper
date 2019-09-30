using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQuery : WrapRequest<Customer>, IRequest<GetCustomerByIDQueryResponse>
    {
        public GetCustomerByIDQuery()
        {
            ConfigKeys(x => x.CustomerID);
            ConfigSuppressedProperties(x => x.Orders);
        }
    }
}
