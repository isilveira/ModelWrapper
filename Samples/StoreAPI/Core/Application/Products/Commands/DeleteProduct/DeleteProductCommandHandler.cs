using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Products.SingleOrDefaultAsync(x => x.ProductID == request.ProductID);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            Context.Products.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new DeleteProductCommandResponseDTO
                {
                    ProductID = data.ProductID,
                    CategoryID = data.CategoryID,
                    Name = data.Name,
                    Description = data.Description,
                    Specifications = data.Specifications,
                    RegistrationDate = data.RegistrationDate,
                    Value = data.Value,
                    Amount = data.Amount,
                    IsVisible = data.IsVisible
                }
            };
        }
    }
}
