using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Orders.Commands.PostOrder
{
    public class PostOrderCommandResponse : CommandResponse<Dictionary<string, object>, PostOrderCommandResposeDTO>
    {
    }
}
