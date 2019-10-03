using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
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
            var data =  await Context.Categories
                .Select<Category>(request)
                //.Filter(request)
                //.Search(request)
                //.Count(ref resultCount)
                //.OrderBy(request)
                //.Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            resultCount = data.Count;

            return new GetCategoriesByFilterQueryResponse(request, data, "Successful operation!", resultCount);
        }
    }
}
