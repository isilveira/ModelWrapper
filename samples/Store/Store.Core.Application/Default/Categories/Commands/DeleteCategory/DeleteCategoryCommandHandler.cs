using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Categories;

namespace Store.Core.Application.Default.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : ApplicationRequestHandler<Category, DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IDeleteCategoryService DeleteService { get; set; }
        public DeleteCategoryCommandHandler(
            IDefaultDbContext context,
            IDeleteCategoryService deleteService)
        {
            Context = context;
            DeleteService = deleteService;
        }
        public override async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
                throw new Exception("Category not found!");

            await DeleteService.Run(data);

            await Context.SaveChangesAsync();

            return new DeleteCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
