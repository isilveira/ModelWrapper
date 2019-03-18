using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Contexts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, DeleteImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public DeleteImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<DeleteImageCommandResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var data = await Context.Images.SingleOrDefaultAsync(x => x.ImageID == request.ImageID);

            if (data == null)
            {
                throw new Exception("Image not found!");
            }

            Context.Images.Remove(data);

            await Context.SaveChangesAsync();

            return new DeleteImageCommandResponse
            {
                Message = "Successful operation!",
                Request = request,
                Data = new DeleteImageCommandResponseDTO
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
