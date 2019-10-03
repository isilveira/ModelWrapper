using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PutProduct
{
    public class PutProductCommand : WrapRequest<Product>, IRequest<PutProductCommandResponse>
    {
        public PutProductCommand()
        {
            ConfigKeys(x => x.ProductID);
            ConfigSuppressedProperties(x => x.RegistrationDate);
            ConfigSuppressedProperties(x => x.Category);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.OrderedProducts);
        }
    }
}
