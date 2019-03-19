using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommand : Wrap<Order, int>, IRequest<PutOrderCommandResponse>
    {
        public PutOrderCommand()
        {
            SuppressProperty(x => x.OrderID);
        }
    }
}
