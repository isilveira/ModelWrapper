using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, PatchProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchProductCommandResponse> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ProductID);

            var data = await Context.Products.SingleOrDefaultAsync(x => x.ProductID == id);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
