using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommand : RequestBase<Image, PostImageCommandResponse>
    {
        public PostImageCommand()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
