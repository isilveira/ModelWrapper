using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PutCategory
{
    public class PutCategoryCommandHandler : IRequestHandler<PutCategoryCommand, PutCategoryCommandResponse>
    {
        public IStoreContext Context { get; set; }
        public PutCategoryCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PutCategoryCommandResponse> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.CategoryID);
            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == id);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            request.Put(data);

            await Context.SaveChangesAsync();

            return new PutCategoryCommandResponse
            {
                Message = "Successful operation!",
                Request = request.AsDictionary(),
                Data = new PutCategoryCommandResponseDTO
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
