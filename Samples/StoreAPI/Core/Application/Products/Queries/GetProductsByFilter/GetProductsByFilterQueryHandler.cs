using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var query = Context.Products.AsQueryable().AsNoTracking();

            if (request.CategoryID.HasValue)
            {
                query = query.Where(x => x.CategoryID == request.CategoryID.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            if (!string.IsNullOrWhiteSpace(request.Specifications))
            {
                query = query.Where(x => x.Specifications.Contains(request.Specifications));
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

            if (request.IsVisible.HasValue)
            {
                query = query.Where(x => x.IsVisible == request.IsVisible.Value);
            }

            int resultCount = await query.CountAsync();

            var results = await query.ToListAsync(cancellationToken);

            return new GetProductsByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetProductsByFilterQueryResponseDTO
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
                }).ToList()
            };
        }
    }
}
