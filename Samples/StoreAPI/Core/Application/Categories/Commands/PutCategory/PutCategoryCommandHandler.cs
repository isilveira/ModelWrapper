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
            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == request.CategoryID);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            data.RootCategoryID = request.RootCategoryID;
            data.Name = request.Name;
            data.Description = request.Description;

            await Context.SaveChangesAsync();

            return new PutCategoryCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
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
