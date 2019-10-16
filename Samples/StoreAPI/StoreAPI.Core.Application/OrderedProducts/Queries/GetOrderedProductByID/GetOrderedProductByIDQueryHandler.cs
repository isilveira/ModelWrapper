using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductByID
{
    public class GetOrderedProductByIDQueryHandler : IRequestHandler<GetOrderedProductByIDQuery, GetOrderedProductByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetOrderedProductByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetOrderedProductByIDQueryResponse> Handle(GetOrderedProductByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.OrderedProductID);

            var data = await Context.OrderedProducts
                .Where(x => x.OrderedProductID == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            return new GetOrderedProductByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
