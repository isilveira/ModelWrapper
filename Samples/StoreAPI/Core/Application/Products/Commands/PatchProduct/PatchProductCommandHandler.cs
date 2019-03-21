using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Commands.PatchProduct
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

            return new PatchProductCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.OnlySupplieds),
                Data = new PatchProductCommandResponseDTO
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
