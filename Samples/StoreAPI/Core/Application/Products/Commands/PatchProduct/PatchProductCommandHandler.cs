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
            var data = await Context.Products.SingleOrDefaultAsync(x => x.ProductID == request.ProductID);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            if (request.Amount.HasValue)
            {
                data.Amount = request.Amount.Value;
            }

            if (request.CategoryID.HasValue)
            {
                data.CategoryID = request.CategoryID.Value;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                data.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                data.Description = request.Description;
            }

            if (!string.IsNullOrWhiteSpace(request.Specifications))
            {
                data.Specifications = request.Specifications;
            }

            if (request.Value.HasValue)
            {
                data.Value = request.Value.Value;
            }

            if (request.IsVisible.HasValue)
            {
                data.IsVisible = request.IsVisible.Value;
            }

            await Context.SaveChangesAsync();

            return new PatchProductCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
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
