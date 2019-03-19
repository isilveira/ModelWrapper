using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommand : Wrap<Order, int>, IRequest<PostOrderCommandResponse>
    {
        public PostOrderCommand()
        {
            SuppressProperty(x => x.OrderID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.CancellationDate);
            SuppressProperty(x => x.ConfirmationDate);
        }
    }
}
