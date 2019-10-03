using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommand : WrapRequest<Image>, IRequest<PostImageCommandResponse>
    {
        public PostImageCommand()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
