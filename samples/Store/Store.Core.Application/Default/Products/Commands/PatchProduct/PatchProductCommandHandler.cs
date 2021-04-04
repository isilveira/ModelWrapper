using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Products;

namespace Store.Core.Application.Default.Products.Commands.PatchProduct
{
    public class PatchProductCommandHandler : ApplicationRequestHandler<Product, PatchProductCommand, PatchProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchProductService PatchService { get; set; }
        public PatchProductCommandHandler(
            IDefaultDbContext context,
            IPatchProductService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchProductCommandResponse> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
