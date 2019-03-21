using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : Wrap<Order>, IRequest<PatchOrderCommandResponse>
    {
        public PatchOrderCommand()
        {
            KeyProperty(x => x.OrderID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.OrderedProducts);
            SuppressProperty(x => x.Customer);
        }
    }
}
