using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommandHandler : IRequestHandler<PutOrderedProductCommand, PutOrderedProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutOrderedProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutOrderedProductCommandResponse> Handle(PutOrderedProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.OrderedProductID);

            var data = await Context.OrderedProducts.SingleOrDefaultAsync(x => x.OrderedProductID == id);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutOrderedProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
