using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommand : WrapRequest<Order>, IRequest<PostOrderCommandResponse>
    {
        public PostOrderCommand()
        {
            ConfigKeys(x => x.OrderID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.CancellationDate);
            ConfigSuppressedProperties(x => x.ConfirmationDate);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Customer);
        }
    }
}
