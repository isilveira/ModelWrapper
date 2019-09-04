using EntitySearch.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int resultCount = 0;
            var results = await Context.Customers
                .Filter(request)
                .Search(request)
                .Count(ref resultCount)
                .OrderBy(request)
                .Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetCustomersByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetCustomersByFilterQueryResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }).ToList()
            };
        }
    }
}
