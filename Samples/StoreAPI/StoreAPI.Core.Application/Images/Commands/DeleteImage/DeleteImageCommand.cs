using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : WrapRequest<Image>, IRequest<DeleteImageCommandResponse>
    {
        public DeleteImageCommand()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
