using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : WrapRequest<Order>, IRequest<PatchOrderCommandResponse>
    {
        public PatchOrderCommand()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
