using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommand : Wrap<OrderedProduct>, IRequest<PutOrderedProductCommandResponse>
    {
        public PutOrderedProductCommand()
        {
            KeyProperty(x => x.OrderedProductID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.Order);
            SuppressProperty(x => x.Product);
        }
    }
}
