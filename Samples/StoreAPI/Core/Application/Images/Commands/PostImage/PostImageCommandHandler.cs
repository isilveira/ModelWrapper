using MediatR;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommandHandler : IRequestHandler<PostImageCommand, PostImageCommandResponse>
    {
        private IStoreContext Context { get; set; }
        public PostImageCommandHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<PostImageCommandResponse> Handle(PostImageCommand request, CancellationToken cancellationToken)
        {
            var data = new Image
            {
                MimeType = request.MimeType,
                ProductID = request.ProductID,
                Url = request.Url
            };

            await Context.Images.AddAsync(data);

            await Context.SaveChangesAsync();

            return new PostImageCommandResponse
            {
                Message = "Successful operation!",
                Request = request,
                Data = new PostImageCommandResponseDTO
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
