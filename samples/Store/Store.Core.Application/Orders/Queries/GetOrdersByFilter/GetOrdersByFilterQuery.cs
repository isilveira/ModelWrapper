using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQuery : RequestBase<Order, GetOrdersByFilterQueryResponse>
    {
        public GetOrdersByFilterQuery()
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
