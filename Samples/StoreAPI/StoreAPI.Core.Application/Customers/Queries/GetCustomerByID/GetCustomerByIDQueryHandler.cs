using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Linq;
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
            var id = request.Project(x => x.CustomerID);

            var data = await Context.Customers
                .Where(x => x.CustomerID == id)
                .Select(x=> new { x.Name })
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Customer not found!");
            }

            return new GetCustomerByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
