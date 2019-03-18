using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var query = Context.Customers.AsQueryable().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(x => x.Email.Contains(request.Email));
            }

            if (request.RegistrationDate.HasValue)
            {
                query = query.Where(x => x.RegistrationDate == request.RegistrationDate.Value);
            }

            int resultCount = await query.CountAsync();
            var results = await query.ToListAsync();

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
