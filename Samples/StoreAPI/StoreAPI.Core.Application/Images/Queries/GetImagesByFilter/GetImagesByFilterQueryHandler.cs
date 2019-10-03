using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
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

            var data = await Context.Images
                .Select<Image>(request)
                //.Filter(request)
                //.Search(request)
                //.Count(ref resultCount)
                //.OrderBy(request)
                //.Scope(request)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            resultCount = data.Count;

            return new GetImagesByFilterQueryResponse(request, data, "Successful operation!", resultCount);
        }
    }
}
