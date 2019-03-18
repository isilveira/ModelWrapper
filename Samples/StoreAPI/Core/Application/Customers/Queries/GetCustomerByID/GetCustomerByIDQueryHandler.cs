using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryHandler : IRequestHandler<GetCustomerByIDQuery, GetCustomerByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetCustomerByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetCustomerByIDQueryResponse> Handle(GetCustomerByIDQuery request, CancellationToken cancellationToken)
        {
            var data = await Context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.CustomerID == request.CustomerID);

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            return new GetCustomerByIDQueryResponse
            {
                ResultCount = 1,
                Request = request,
                Data = new GetCustomerByIDQueryResponseDTO
                {
                    CustomerID = data.CustomerID,
                    Name = data.Name,
                    Email = data.Email
                }
            };
        }
    }
}
