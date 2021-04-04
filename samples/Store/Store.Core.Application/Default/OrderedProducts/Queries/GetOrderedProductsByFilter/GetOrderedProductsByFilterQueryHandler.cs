using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQueryHandler : IRequestHandler<GetOrderedProductsByFilterQuery, GetOrderedProductsByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetOrderedProductsByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetOrderedProductsByFilterQueryResponse> Handle(GetOrderedProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.OrderedProducts
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetOrderedProductsByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
