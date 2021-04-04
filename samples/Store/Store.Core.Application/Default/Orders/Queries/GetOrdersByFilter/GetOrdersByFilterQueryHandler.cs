using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryHandler : IRequestHandler<GetOrdersByFilterQuery, GetOrdersByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetOrdersByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetOrdersByFilterQueryResponse> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Orders
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetOrdersByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
