using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : WrapRequest<Product>, IRequest<DeleteProductCommandResponse>
    {
        public DeleteProductCommand()
        {
            ConfigKeys(x => x.ProductID);
            ConfigSuppressedProperties(x => x.OrderedProducts);
            ConfigSuppressedProperties(x => x.Images);
            ConfigSuppressedProperties(x => x.Category);
        }
    }
}
