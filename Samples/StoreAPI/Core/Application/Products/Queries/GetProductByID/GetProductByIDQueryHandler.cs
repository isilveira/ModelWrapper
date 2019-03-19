using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var data = await Context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.ProductID == request.ProductID);

            if (data == null)
            {
                throw new Exception("Product not found!");
            }

            return new GetProductByIDQueryResponse
            {
                ResultCount = 1,
                Request = request,
                Data = new GetProductByIDQueryResponseDTO
                {
                    ProductID = data.ProductID,
                    CategoryID = data.CategoryID,
                    Name = data.Name,
                    Description = data.Description,
                    Specifications = data.Specifications,
                    RegistrationDate = data.RegistrationDate,
                    Value = data.Value,
                    Amount = data.Amount,
                    IsVisible = data.IsVisible
                }
            };
        }
    }
}
