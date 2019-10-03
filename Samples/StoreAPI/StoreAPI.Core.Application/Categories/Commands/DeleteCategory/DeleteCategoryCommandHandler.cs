using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
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
            var id = request.Project(x => x.CategoryID);

            var data = await Context.Categories.SingleOrDefaultAsync(x => x.CategoryID == id);

            if (data == null)
                throw new Exception("Category not found!");

            Context.Categories.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
