using EntitySearch.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoriesByFilter
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
            var results =  await Context.Categories
                .Filter(request)
                .Search(request)
                .Count(ref resultCount)
                .OrderBy(request)
                .Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetCategoriesByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetCategoriesByFilterQueryResponseDTO
                {
                    CategoryID = data.CategoryID,
                    RootCategoryID = data.RootCategoryID,
                    Name = data.Name,
                    Description = data.Description
                }).ToList()
            };
        }
    }
}
