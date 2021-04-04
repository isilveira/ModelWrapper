using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Queries.GetImagesByFilter
{
    public class GetImagesByFilterQueryResponse : ApplicationResponse<Image>
    {
        public GetImagesByFilterQueryResponse(WrapRequest<Image> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
