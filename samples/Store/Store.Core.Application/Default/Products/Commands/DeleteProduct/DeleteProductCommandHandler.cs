using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Products;

namespace Store.Core.Application.Default.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : ApplicationRequestHandler<Product, DeleteProductCommand, DeleteProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteProductService DeleteService { get; set; }
        public DeleteProductCommandHandler(
            IDefaultDbContext context,
            IDeleteProductService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Product not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
