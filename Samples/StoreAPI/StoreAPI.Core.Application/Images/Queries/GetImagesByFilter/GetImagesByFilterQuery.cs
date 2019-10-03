using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQuery : WrapRequest<Image>, IRequest<GetImagesByFilterQueryResponse>
    {
        public GetImagesByFilterQuery()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
