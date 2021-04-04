using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.PutImage
{
    public class PutImageCommand : ApplicationRequest<Image, PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
