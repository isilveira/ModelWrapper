using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Samples.Commands.DeleteSample
{
    public class DeleteSampleCommand : ApplicationRequest<Sample, DeleteSampleCommandResponse>
    {
        public DeleteSampleCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            //ConfigSuppressedProperties(x => x);
            //ConfigSuppressedResponseProperties(x => x);
        }
    }
}
