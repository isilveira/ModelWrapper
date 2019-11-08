using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommand : WrapRequest<Image>, IRequest<PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
