using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;

namespace Store.Core.Application.Default.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandHandler : ApplicationRequestHandler<OrderedProduct, PatchOrderedProductCommand, PatchOrderedProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchOrderedProductService PatchService { get; set; }
        public PatchOrderedProductCommandHandler(
            IDefaultDbContext context,
            IPatchOrderedProductService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchOrderedProductCommandResponse> Handle(PatchOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
