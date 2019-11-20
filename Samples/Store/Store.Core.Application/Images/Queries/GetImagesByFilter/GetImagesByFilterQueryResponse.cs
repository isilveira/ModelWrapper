using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryResponse : WrapResponse<Image>
    {
        public GetImagesByFilterQueryResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
