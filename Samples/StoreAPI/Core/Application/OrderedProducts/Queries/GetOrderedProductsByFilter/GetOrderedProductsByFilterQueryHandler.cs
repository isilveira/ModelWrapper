using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var query = Context.OrderedProducts.AsQueryable().AsNoTracking();

            if (request.OrderID.HasValue)
            {
                query = query.Where(x => x.OrderID == request.OrderID.Value);
            }

            if (request.ProductID.HasValue)
            {
                query = query.Where(x => x.ProductID == request.ProductID.Value);
            }

            if (request.Amount.HasValue)
            {
                query = query.Where(x => x.Amount == request.Amount.Value);
            }

            if (request.Value.HasValue)
            {
                query = query.Where(x => x.Value == request.Value.Value);
            }

            if (request.RegistrationDate.HasValue)
            {
                query = query.Where(x => x.RegistrationDate == request.RegistrationDate.Value);
            }

            int resultCount = await query.CountAsync();
            var results = await query.ToListAsync();

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
