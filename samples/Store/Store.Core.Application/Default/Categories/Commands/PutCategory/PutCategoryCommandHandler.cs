using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Put;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Services.Default.Categories;

namespace Store.Core.Application.Default.Categories.Commands.PutCategory
{
    public class PutCategoryCommandHandler : ApplicationRequestHandler<Category, PutCategoryCommand, PutCategoryCommandResponse>
    {
        public IDefaultDbContext Context { get; set; }
        private IPutCategoryService PutService { get; set; }
        public PutCategoryCommandHandler(
            IDefaultDbContext context,
            IPutCategoryService putService)
        {
            Context = context;
            PutService = putService;
        }
        public override async Task<PutCategoryCommandResponse> Handle(PutCategoryCommand request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);
            var data = await Context.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            request.Put(data);

            await PutService.Run(data);

            await Context.SaveChangesAsync();

            return new PutCategoryCommandResponse(request, data, "Successful operation!", 1);
        }
    }
}
