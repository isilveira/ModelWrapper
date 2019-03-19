using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommand : Wrap<Image,int>, IRequest<PostImageCommandResponse>
    {
        public PostImageCommand()
        {
            SuppressProperty(x => x.ImageID);
        }
    }
}
