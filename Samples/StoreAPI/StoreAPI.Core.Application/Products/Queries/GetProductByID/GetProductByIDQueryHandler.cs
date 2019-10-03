using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Queries.GetProductByID
{
    public class GetProductByIDQueryHandler : IRequestHandler<GetProductByIDQuery, GetProductByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetProductByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetProductByIDQueryResponse> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ProductID);

            var data = await Context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.ProductID == id);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            return new GetProductByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
