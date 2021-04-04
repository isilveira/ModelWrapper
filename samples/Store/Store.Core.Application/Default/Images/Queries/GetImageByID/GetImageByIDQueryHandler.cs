using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Default.Images.Queries.GetImageByID
{
    public class GetImageByIDQueryHandler : IRequestHandler<GetImageByIDQuery, GetImageByIDQueryResponse>
    {
        private IDefaultDbContext Context { get; set; }
        public GetImageByIDQueryHandler(IDefaultDbContext context)
        {
            Context = context;
        }
        public async Task<GetImageByIDQueryResponse> Handle(GetImageByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.Id);

            var data = await Context.Images
                .Where(x => x.Id == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            return new GetImageByIDQueryResponse(request, data, resultCount: 1);
        }
    }
}
