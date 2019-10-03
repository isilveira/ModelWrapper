using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Images.Commands.DeleteImage
{
    public class DeleteImageCommandResponse : WrapResponse<Image>
    {
        public DeleteImageCommandResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
