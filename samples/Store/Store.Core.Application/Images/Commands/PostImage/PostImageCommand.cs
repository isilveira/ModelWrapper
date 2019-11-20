using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PostImage
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
