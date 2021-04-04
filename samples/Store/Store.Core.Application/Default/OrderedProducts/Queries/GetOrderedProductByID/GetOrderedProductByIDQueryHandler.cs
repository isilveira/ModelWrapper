using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQueryHandler : IRequestHandler<GetOrderedProductByIDQuery, GetOrderedProductByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetOrderedProductByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetOrderedProductByIDQueryResponse> Handle(GetOrderedProductByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.OrderedProducts
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            return new GetOrderedProductByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
