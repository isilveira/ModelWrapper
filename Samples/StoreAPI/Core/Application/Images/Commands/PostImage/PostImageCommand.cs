using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommand : Wrap<Image>, IRequest<PostImageCommandResponse>
    {
        public PostImageCommand()
        {
            KeyProperty(x => x.ImageID);
            SuppressProperty(x => x.Product);
        }
    }
}
