using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommand : RequestBase<Image, PatchImageCommandResponse>
    {
        public PatchImageCommand()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
