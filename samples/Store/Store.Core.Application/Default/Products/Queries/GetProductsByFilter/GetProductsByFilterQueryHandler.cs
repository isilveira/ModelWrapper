using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQueryHandler : IRequestHandler<GetProductsByFilterQuery, GetProductsByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetProductsByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetProductsByFilterQueryResponse> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Products
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetProductsByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
