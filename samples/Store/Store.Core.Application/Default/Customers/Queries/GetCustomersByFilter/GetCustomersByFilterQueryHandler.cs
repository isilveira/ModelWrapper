using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryHandler : IRequestHandler<GetCustomersByFilterQuery, GetCustomersByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetCustomersByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetCustomersByFilterQueryResponse> Handle(GetCustomersByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Customers
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetCustomersByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
