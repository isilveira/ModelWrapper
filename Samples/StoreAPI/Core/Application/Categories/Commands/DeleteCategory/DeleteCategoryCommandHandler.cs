using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        public IStoreContext Context { get; set; }
        public DeleteCategoryCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == request.CategoryID);

            if (data == null)
                throw new Exception("Category not found!");

            Context.Categories.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteCategoryCommandResponse
            {
                Request = request,
                Message = "Successful operation!",
                Data = new DeleteCategoryCommandResponseDTO
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
