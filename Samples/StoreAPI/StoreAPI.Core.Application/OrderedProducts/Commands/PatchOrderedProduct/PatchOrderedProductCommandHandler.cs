using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandHandler : IRequestHandler<PatchOrderedProductCommand, PatchOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchOrderedProductCommandResponse> Handle(PatchOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.OrderedProductID);
            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == id);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
