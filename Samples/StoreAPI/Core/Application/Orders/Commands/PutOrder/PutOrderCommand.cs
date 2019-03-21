using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommand : Wrap<Order>, IRequest<PutOrderCommandResponse>
    {
        public PutOrderCommand()
        {
            KeyProperty(x => x.OrderID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.OrderedProducts);
            SuppressProperty(x => x.Customer);
        }
    }
}
