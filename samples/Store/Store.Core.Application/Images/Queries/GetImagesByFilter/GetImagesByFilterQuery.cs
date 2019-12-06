using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQuery : RequestBase<Image, GetImagesByFilterQueryResponse>
    {
        public GetImagesByFilterQuery()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
