using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQueryHandler : IRequestHandler<GetCategoriesByFilterQuery, GetCategoriesByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetCategoriesByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetCategoriesByFilterQueryResponse> Handle(GetCategoriesByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Categories
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetCategoriesByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
