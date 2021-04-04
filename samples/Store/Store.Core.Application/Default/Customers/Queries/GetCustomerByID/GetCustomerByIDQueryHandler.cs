using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Customers.Queries.GetCustomerByID
{
    public class GetCustomerByIDQueryHandler : IRequestHandler<GetCustomerByIDQuery, GetCustomerByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetCustomerByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetCustomerByIDQueryResponse> Handle(GetCustomerByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Customers
                .Where(x => x.Id == id)
                .Select(request)
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
