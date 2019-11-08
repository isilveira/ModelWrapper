﻿using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Orders.Commands.PutOrder
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