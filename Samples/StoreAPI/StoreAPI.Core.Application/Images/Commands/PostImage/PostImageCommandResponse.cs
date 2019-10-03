using ModelWrapper;
using StoreAPI.Core.Application.Bases;
using StoreAPI.Core.Domain.Entities;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Images.Commands.PostImage
{
    public class PostImageCommandResponse : WrapResponse<Image>
    {
        public PostImageCommandResponse(WrapRequest<Image> request, object data, string message = null, long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
