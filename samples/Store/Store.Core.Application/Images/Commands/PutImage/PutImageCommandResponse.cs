using ModelWrapper;
using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommandResponse : ResponseBase<Image>
    {
        public PutImageCommandResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
