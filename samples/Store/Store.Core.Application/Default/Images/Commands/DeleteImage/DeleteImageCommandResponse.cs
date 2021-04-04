using ModelWrapper;
using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Images.Commands.DeleteImage
{
    public class DeleteImageCommandResponse : ApplicationResponse<Image>
    {
        public DeleteImageCommandResponse(WrapRequest<Image> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
