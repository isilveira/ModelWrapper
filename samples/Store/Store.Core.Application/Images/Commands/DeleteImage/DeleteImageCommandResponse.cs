using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommandResponse : WrapResponse<Image>
    {
        public DeleteImageCommandResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
