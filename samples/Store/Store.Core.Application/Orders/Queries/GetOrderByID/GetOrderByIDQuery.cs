using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQuery : WrapRequest<Order>, IRequest<GetOrderByIDQueryResponse>
    {
        protected GetOrderByIDQuery()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
            ConfigSuppressedResponseProperties(x => x.OrderedProducts);
            ConfigSuppressedResponseProperties(x => x.Customer);
        }
    }
}
