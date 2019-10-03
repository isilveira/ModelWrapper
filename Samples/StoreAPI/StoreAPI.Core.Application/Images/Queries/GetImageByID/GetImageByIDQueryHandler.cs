using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Queries.GetImageByID
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

            var data = await Context.Images.AsNoTracking().SingleOrDefaultAsync(x => x.ImageID == id);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            return new GetImageByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
