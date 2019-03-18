using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryHandler : IRequestHandler<GetImagesByFilterQuery, GetImagesByFilterQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetImagesByFilterQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetImagesByFilterQueryResponse> Handle(GetImagesByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = Context.Images.AsQueryable().AsNoTracking();

            if (request.ProductID.HasValue)
            {
                query = query.Where(x => x.ProductID == request.ProductID.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.MimeType))
            {
                query = query.Where(x => x.MimeType.Contains(request.MimeType));
            }

            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                query = query.Where(x => x.Url.Contains(request.Url));
            }

            int resultCount = await query.CountAsync();

            var results = await query.ToListAsync(cancellationToken);

            return new GetImagesByFilterQueryResponse
            {
                Request = request,
                ResultCount = resultCount,
                Data = results.Select(data => new GetImagesByFilterQueryResponseDTO
                {
                    ImageID = data.ImageID,
                    ProductID = data.ProductID,
                    MimeType = data.MimeType,
                    Url = data.Url
                }).ToList()
            };
        }
    }
}
