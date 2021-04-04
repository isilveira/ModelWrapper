using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Queries.GetImageByID
{
    public class GetImageByIDQuery : ApplicationRequest<Image, GetImageByIDQueryResponse>
    {
        public GetImageByIDQuery()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
