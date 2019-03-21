using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommand : Wrap<OrderedProduct>, IRequest<PatchOrderedProductCommandResponse>
    {
        public PatchOrderedProductCommand()
        {
            KeyProperty(x => x.OrderedProductID);
            SuppressProperty(x => x.RegistrationDate);
            SuppressProperty(x => x.Order);
            SuppressProperty(x => x.Product);
        }
    }
}
