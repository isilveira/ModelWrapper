using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommand : RequestBase<Image, PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
