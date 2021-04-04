using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Patch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Categories;

namespace Store.Core.Application.Default.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommandHandler : ApplicationRequestHandler<Category, PatchCategoryCommand, PatchCategoryCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPatchCategoryService PatchService { get; set; }
        public PatchCategoryCommandHandler(
            IDefaultDbContext context,
            IPatchCategoryService patchService)
        {
            Context = context;
            PatchService = patchService;
        }
        public override async Task<PatchCategoryCommandResponse> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            request.Patch(data);

            await PatchService.Run(data);

            await Context.SaveChangesAsync();

            return new PatchCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
