using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryHandler : IRequestHandler<GetProductByIDQuery, GetProductByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetProductByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetProductByIDQueryResponse> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Products
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            return new GetProductByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
