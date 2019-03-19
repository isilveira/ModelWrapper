using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrdersByFilter
{
    public class GetOrdersByFilterQueryHandler : IRequestHandler<GetOrdersByFilterQuery, GetOrdersByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetOrdersByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetOrdersByFilterQueryResponse> Handle(GetOrdersByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Orders.AsQueryable().AsNoTracking();

            if (request.CustomerID.HasValue)
            {
                query = query.Where(x => x.CustomerID == request.CustomerID.Value);
            }

            if (request.RegistrationDate.HasValue)
            {
                query = query.Where(x => x.RegistrationDate == request.RegistrationDate.Value);
            }

            if (request.CancellationDate.HasValue)
            {
                query = query.Where(x => x.CancellationDate == request.CancellationDate.Value);
            }

            if (request.ConfirmationDate.HasValue)
            {
                query = query.Where(x => x.ConfirmationDate == request.ConfirmationDate.Value);
            }

            int resultCount = await query.CountAsync();
            var results = await query.ToListAsync();

            return new GetOrdersByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetOrdersByFilterQueryResponseDTO
                {
                    OrderID = data.OrderID,
                    CustomerID = data.CustomerID,
                    RegistrationDate = data.RegistrationDate,
                    ConfirmationDate = data.ConfirmationDate,
                    CancellationDate = data.CancellationDate
                }).ToList()
            };
        }
    }
}
