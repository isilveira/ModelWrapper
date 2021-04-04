using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Products;

namespace Store.Core.Application.Default.Products.Commands.PutProduct
{
    public class PutProductCommandHandler : ApplicationRequestHandler<Product, PutProductCommand, PutProductCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutProductService PutService { get; set; }
        public PutProductCommandHandler(
            IDefaultDbContext context,
            IPutProductService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutProductCommandResponse> Handle(PutProductCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutProductCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
