using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommand : Wrap<Image>, IRequest<PatchImageCommandResponse>
    {
        public PatchImageCommand()
        {
            KeyProperty(x => x.ImageID);
            SuppressProperty(x => x.Product);
        }
    }
}
