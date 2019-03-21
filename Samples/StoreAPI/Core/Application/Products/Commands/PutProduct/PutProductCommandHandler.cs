using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PutProduct
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

            return new PutProductCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(),
                Data = new PutProductCommandResponseDTO
                {
                    ProductID = data.ProductID,
                    CategoryID = data.CategoryID,
                    Name = data.Name,
                    Description = data.Description,
                    Specifications = data.Specifications,
                    RegistrationDate = data.RegistrationDate,
                    Value = data.Value,
                    Amount = data.Amount,
                    IsVisible = data.IsVisible
                }
            };
        }
    }
}
