using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommand : WrapRequest<Order>, IRequest<PutOrderCommandResponse>
    {
        public PutOrderCommand()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
