using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommand : Wrap<OrderedProduct, int>, IRequest<PutOrderedProductCommandResponse>
    {
        public PutOrderedProductCommand()
        {
            SuppressProperty(x => x.OrderedProductID);
            SuppressProperty(x => x.RegistrationDate);
        }
    }
}
