using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.OrderedProducts.Queries.GetOrderedProductsByFilter
{
    public class GetOrderedProductsByFilterQueryHandler : IRequestHandler<GetOrderedProductsByFilterQuery, GetOrderedProductsByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetOrderedProductsByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetOrderedProductsByFilterQueryResponse> Handle(GetOrderedProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            int resultCount = 0;

            var data = await Context.OrderedProducts
                .Select<OrderedProduct>(request)
                //.Filter(request)
                //.Search(request)
                //.Count(ref resultCount)
                //.OrderBy(request)
                //.Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            resultCount = data.Count;

            return new GetOrderedProductsByFilterQueryResponse(request, data, "Successful operation!", resultCount);
        }
    }
}
