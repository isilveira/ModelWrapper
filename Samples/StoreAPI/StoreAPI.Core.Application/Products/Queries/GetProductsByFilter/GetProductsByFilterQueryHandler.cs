using EntitySearch.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQueryHandler : IRequestHandler<GetProductsByFilterQuery, GetProductsByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetProductsByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }

        public async Task<GetProductsByFilterQueryResponse> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
        {
            int resultCount = 0;
            var data = await Context.Products
                .Select<Product>(request)
                //.Filter(request)
                //.Search(request)
                //.Count(ref resultCount)
                //.OrderBy(request)
                //.Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            resultCount = data.Count;

            return new GetProductsByFilterQueryResponse(request, data, "Successful operation!", resultCount);
        }
    }
}
