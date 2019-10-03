using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommand : WrapRequest<OrderedProduct>, IRequest<PutOrderedProductCommandResponse>
    {
        public PutOrderedProductCommand()
        {
            ConfigKeys(x => x.OrderedProductID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Order);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
