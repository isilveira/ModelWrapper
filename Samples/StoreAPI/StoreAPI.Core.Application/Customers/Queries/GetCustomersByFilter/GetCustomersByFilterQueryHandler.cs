using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomersByFilter
{
    public class GetCustomersByFilterQueryHandler : IRequestHandler<GetCustomersByFilterQuery, GetCustomersByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetCustomersByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetCustomersByFilterQueryResponse> Handle(GetCustomersByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            var results = await Context.Customers
                .Select(request)
                //.Filter(request)
                //.Search(request)
                //.Count(ref resultCount)
                //.OrderBy(request)
                //.Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            resultCount = results.Count;

            return new GetCustomersByFilterQueryResponse(request, results, resultCount: resultCount);
        }
    }
}
