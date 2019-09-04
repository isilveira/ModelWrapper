using EntitySearch.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
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
            int resultCount = 0;

            var results = await Context.Images
                .Filter(request)
                .Search(request)
                .Count(ref resultCount)
                .OrderBy(request)
                .Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

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
