using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQueryHandler : IRequestHandler<GetOrderByIDQuery, GetOrderByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetOrderByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetOrderByIDQueryResponse> Handle(GetOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Orders
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            return new GetOrderByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
