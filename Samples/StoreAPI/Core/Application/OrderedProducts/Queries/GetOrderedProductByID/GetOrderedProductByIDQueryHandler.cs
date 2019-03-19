using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
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
            var data = await Context.OrderedProducts.AsNoTracking().SingleOrDefaultAsync(x => x.OrderedProductID== request.OrderedProductID);

            if (data == null)
            {
                throw new Exception("OrderedProduct not found!");
            }

            return new GetOrderedProductByIDQueryResponse
            {
                ResultCount = 1,
                Request = request,
                Data = new GetOrderedProductByIDQueryResponseDTO
                {
                    OrderedProductID = data.OrderedProductID,
                    OrderID = data.OrderID,
                    ProductID = data.ProductID,
                    Amount = data.Amount,
                    Value = data.Value,
                    RegistrationDate = data.RegistrationDate
                }
            };
        }
    }
}
