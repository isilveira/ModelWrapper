using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQueryHandler : IRequestHandler<GetCategoriesByFilterQuery, GetCategoriesByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetCategoriesByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetCategoriesByFilterQueryResponse> Handle(GetCategoriesByFilterQuery request, CancellationToken cancellationToken)
        {
            int resultCount = 0;

            var data =  await Context.Categories
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetCategoriesByFilterQueryResponse(request, data, "Successful operation!", resultCount);
        }
    }
}
