using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.PostImage
{
    public class PostImageCommand : ApplicationRequest<Image, PostImageCommandResponse>
    {
        public PostImageCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
