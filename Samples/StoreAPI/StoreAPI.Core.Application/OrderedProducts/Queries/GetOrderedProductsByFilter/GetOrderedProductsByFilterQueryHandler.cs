using EntitySearch.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System.Linq;
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
            var results = await Context.OrderedProducts
                .Filter(request)
                .Search(request)
                .Count(ref resultCount)
                .OrderBy(request)
                .Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetOrderedProductsByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetOrderedProductsByFilterQueryResponseDTO
                {
                    OrderedProductID = data.OrderedProductID,
                    OrderID = data.OrderID,
                    ProductID = data.ProductID,
                    Amount = data.Amount,
                    Value = data.Value,
                    RegistrationDate = data.RegistrationDate
                }).ToList()
            };
        }
    }
}
