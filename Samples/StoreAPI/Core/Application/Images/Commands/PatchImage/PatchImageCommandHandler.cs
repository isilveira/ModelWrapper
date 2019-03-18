using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommandHandler : IRequestHandler<PatchImageCommand, PatchImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PatchImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PatchImageCommandResponse> Handle(PatchImageCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == request.ImageID);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            if (!string.IsNullOrWhiteSpace(request.MimeType))
            {
                data.MimeType = request.MimeType;
            }

            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                data.Url = request.Url;
            }

            if (request.ProductID.HasValue)
            {
                data.ProductID = request.ProductID.Value;
            }

            await Context.SaveChangesAsync();

            return new PatchImageCommandResponse
            {
                Message = "Successful operation!",
                Request = request,
                Data = new PatchImageCommandResponseDTO
                {
                    ImageID = data.ImageID,
                    MimeType = data.MimeType,
                    ProductID = data.ProductID,
                    Url = data.Url
                }
            };
        }
    }
}
