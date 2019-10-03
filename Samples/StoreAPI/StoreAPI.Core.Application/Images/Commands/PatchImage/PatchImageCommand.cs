using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PatchImage
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
