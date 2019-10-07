using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Linq;
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

            var data = await Context.Products
                .Where(x => x.ProductID == id)
                .Select<Product>(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null) throw new Exception("Product not found!");

            return new GetProductByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
