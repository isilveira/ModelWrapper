using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommandHandler : ApplicationRequestHandler<OrderedProduct, PutOrderedProductCommand, PutOrderedProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutOrderedProductService PutService { get; set; }
        public PutOrderedProductCommandHandler(
            IDefaultDbContext context,
            IPutOrderedProductService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutOrderedProductCommandResponse> Handle(PutOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
