using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommandHandler : IRequestHandler<PatchCategoryCommand, PatchCategoryCommandResponse>
    {
        public IStoreContext Context { get; set; }
        public PatchCategoryCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchCategoryCommandResponse> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == request.CategoryID);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            // This "patch" implementation will never allow to set null value on RootCategory
            // becouse there's no way no know if the value was or not supplied
            if (request.RootCategoryID.HasValue)
            {
                data.RootCategoryID = request.RootCategoryID.Value;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                data.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                data.Description = request.Description;
            }

            await Context.SaveChangesAsync();

            return new PatchCategoryCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new PatchCategoryCommandResponseDTO
                {
                    CategoryID = data.CategoryID,
                    RootCategoryID = data.RootCategoryID,
                    Name = data.Name,
                    Description = data.Description
                }
            };
        }
    }
}
