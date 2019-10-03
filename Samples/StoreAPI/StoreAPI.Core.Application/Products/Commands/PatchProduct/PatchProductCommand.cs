using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommand : WrapRequest<Product>, IRequest<PatchProductCommandResponse>
    {
        public PatchProductCommand()
        {
            ConfigKeys(x => x.ProductID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Category);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.OrderedProducts);
        }
    }
}
