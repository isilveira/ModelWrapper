using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
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
            var data = await Context.Images.AsNoTracking().SingleOrDefaultAsync(x => x.ImageID == request.ImageID);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            return new GetImageByIDQueryResponse
            {
                ResultCount = 1,
                Request = request,
                Data = new GetImageByIDQueryResponseDTO
                {
                    ImageID = data.ImageID,
                    ProductID = data.ProductID,
                    MimeType = data.MimeType,
                    Url = data.Url
                }
            };
        }
    }
}
