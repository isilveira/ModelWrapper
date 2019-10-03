using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
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
            var id = request.Project(x => x.ProductID);

            var data = await Context.Products.SingleOrDefaultAsync(x => x.ProductID == id);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            Context.Products.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
