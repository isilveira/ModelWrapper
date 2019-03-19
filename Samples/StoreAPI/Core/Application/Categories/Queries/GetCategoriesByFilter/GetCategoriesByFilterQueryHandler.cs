using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var query = Context.Categories.AsQueryable().AsNoTracking();

            if (request.RootCategoryID.HasValue)
            {
                query = query.Where(x => x.RootCategoryID == request.RootCategoryID.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            int resultCount = await query.CountAsync();
            var results = await query.ToListAsync();

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
