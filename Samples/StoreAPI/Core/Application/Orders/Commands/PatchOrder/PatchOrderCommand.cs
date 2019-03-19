using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommand : Wrap<Order, int>, IRequest<PatchOrderCommandResponse>
    {
        public PatchOrderCommand()
        {
            SuppressProperty(x => x.OrderID);
        }
    }
}
