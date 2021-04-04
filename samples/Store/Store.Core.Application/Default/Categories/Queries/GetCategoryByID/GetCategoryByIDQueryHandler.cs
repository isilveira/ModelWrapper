using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQueryHandler : IRequestHandler<GetCategoryByIDQuery, GetCategoryByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetCategoryByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetCategoryByIDQueryResponse> Handle(GetCategoryByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Categories
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            return new GetCategoryByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
