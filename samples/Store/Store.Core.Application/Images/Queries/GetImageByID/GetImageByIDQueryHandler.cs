using MediatR;
using Microsoft.EntityFrameworkCore;
using ModelWrapper.Extensions.Select;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQueryHandler : IRequestHandler<GetImageByIDQuery, GetImageByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetImageByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetImageByIDQueryResponse> Handle(GetImageByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.ImageID);

            var data = await Context.Images
                .Where(x => x.ImageID == id)
                .Select(request)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            return new GetImageByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
