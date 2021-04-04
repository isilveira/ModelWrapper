using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : ApplicationRequest<Image, DeleteImageCommandResponse>
    {
        public DeleteImageCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);
            
            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
