using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int ProductID { get; set; }

        public DeleteProductCommand()
        {
        }
    }
}
