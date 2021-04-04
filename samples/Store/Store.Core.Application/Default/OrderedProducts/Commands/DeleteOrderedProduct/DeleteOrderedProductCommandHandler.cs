using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;

namespace Store.Core.Application.Default.OrderedProducts.Commands.DeleteOrderedProduct
{
    public class DeleteOrderedProductCommandHandler : ApplicationRequestHandler<OrderedProduct, DeleteOrderedProductCommand, DeleteOrderedProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteOrderedProductService DeleteService { get; set; }
        public DeleteOrderedProductCommandHandler(
            IDefaultDbContext context,
            IDeleteOrderedProductService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteOrderedProductCommandResponse> Handle(DeleteOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("OrderedProduct not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
