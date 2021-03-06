using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Samples.Queries.GetSampleByID
{
    public class GetSampleByIDQuery : ApplicationRequest<Sample, GetSampleByIDQueryResponse>
    {
        public GetSampleByIDQuery()
        {
            ConfigKeys(x => x.Id);
            
            // Configures supressed properties & response properties
            //ConfigSuppressedProperties(x => x);
            //ConfigSuppressedResponseProperties(x => x);  
        }
    }
}
