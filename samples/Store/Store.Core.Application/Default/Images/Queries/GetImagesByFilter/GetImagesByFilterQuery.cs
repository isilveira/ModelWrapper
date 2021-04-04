using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQuery : ApplicationRequest<Image, GetImagesByFilterQueryResponse>
    {
        public GetImagesByFilterQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
