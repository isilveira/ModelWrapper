using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommand : Wrap<OrderedProduct, int>, IRequest<PatchOrderedProductCommandResponse>
    {
        public PatchOrderedProductCommand()
        {
            SuppressProperty(x => x.OrderedProductID);
            SuppressProperty(x => x.RegistrationDate);
        }
    }
}
