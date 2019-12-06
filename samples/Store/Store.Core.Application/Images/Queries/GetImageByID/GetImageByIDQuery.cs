using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQuery : RequestBase<Image, GetImageByIDQueryResponse>
    {
        public GetImageByIDQuery()
        {
            ConfigKeys(x => x.ImageID);

            ConfigSuppressedProperties(x => x.Product);

            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
