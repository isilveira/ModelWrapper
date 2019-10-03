using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQuery : WrapRequest<Order>, IRequest<GetOrderByIDQueryResponse>
    {
        protected GetOrderByIDQuery()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
