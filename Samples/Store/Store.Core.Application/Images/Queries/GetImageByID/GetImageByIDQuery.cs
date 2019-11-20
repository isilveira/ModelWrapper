using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQuery : WrapRequest<Image>, IRequest<GetImageByIDQueryResponse>
    {
        public GetImageByIDQuery()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
