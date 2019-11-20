using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommand : WrapRequest<Image>, IRequest<PatchImageCommandResponse>
    {
        public PatchImageCommand()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
