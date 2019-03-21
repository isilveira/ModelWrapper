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
            var id = request.Project(x => x.CategoryID);

            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == id);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            request.Patch(data);

            await Context.SaveChangesAsync();

            return new PatchCategoryCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(ModelWrapper.EnumProperties.OnlySupplieds),
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
