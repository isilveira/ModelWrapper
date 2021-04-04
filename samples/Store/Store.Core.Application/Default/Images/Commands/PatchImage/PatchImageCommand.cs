using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.PatchImage
{
    public class PatchImageCommand : ApplicationRequest<Image, PatchImageCommandResponse>
    {
        public PatchImageCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
