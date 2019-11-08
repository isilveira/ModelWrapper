using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQuery : WrapRequest<Image>, IRequest<GetImagesByFilterQueryResponse>
    {
        public GetImagesByFilterQuery()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
            ConfigSuppressedResponseProperties(x => x.Product);
        }
    }
}
