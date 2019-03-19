using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.PatchImage
{
    public class PatchImageCommand : Wrap<Image,int>, IRequest<PatchImageCommandResponse>
    {
        public PatchImageCommand()
        {
            SuppressProperty(x => x.ImageID);
        }
    }
}
