using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Products.Commands.PutProduct
{
    public class PutProductCommandHandler : IRequestHandler<PutProductCommand, PutProductCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PutProductCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutProductCommandResponse> Handle(PutProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ProductID);

            var data = await Context.Products.SingleOrDefaultAsync(x => x.ProductID == id);

            if (data == null)
                throw new Exception("Product not found!");

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
