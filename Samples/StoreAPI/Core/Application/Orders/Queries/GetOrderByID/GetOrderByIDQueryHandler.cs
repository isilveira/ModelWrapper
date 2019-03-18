using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;

namespace StoreAPI.Core.Application.Orders.Queries.GetOrderByID
{
    public class GetOrderByIDQueryHandler : IRequestHandler<GetOrderByIDQuery, GetOrderByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetOrderByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetOrderByIDQueryResponse> Handle(GetOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var data = await Context.Orders.AsNoTracking().SingleOrDefaultAsync(x => x.OrderID == request.OrderID);

            if (data == null)
            {
                throw new Exception("Order not found!");
            }

            return new GetOrderByIDQueryResponse
            {
                ResultCount = 1,
                Request = request,
                Data = new GetOrderByIDQueryResponseDTO
                {
                    OrderID = data.OrderID,
                    CustomerID = data.CustomerID,
                    RegistrationDate = data.RegistrationDate,
                    ConfirmationDate = data.ConfirmationDate,
                    CancellationDate = data.CancellationDate
                }
            };
        }
    }
}
