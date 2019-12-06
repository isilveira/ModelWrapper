using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : RequestBase<Image, DeleteImageCommandResponse>
    {
        public DeleteImageCommand()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
