using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.FullSearch;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryHandler : IRequestHandler<GetImagesByFilterQuery, GetImagesByFilterQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetImagesByFilterQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetImagesByFilterQueryResponse> Handle(GetImagesByFilterQuery request, CancellationToken cancellationToken)
        {
            long resultCount = 0;
            
            var data =  await Context.Images
                .FullSearch(request, out resultCount)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            
            return new GetImagesByFilterQueryResponse(request, data, resultCount: resultCount);
        }
    }
}
