using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQueryResponse : WrapResponse<Image>
    {
        public GetImageByIDQueryResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
