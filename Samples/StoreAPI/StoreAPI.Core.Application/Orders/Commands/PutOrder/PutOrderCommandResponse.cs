using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Orders.Commands.PutOrder
{
    public class PutOrderCommandResponse : CommandResponse<Dictionary<string, object>, PutOrderCommandResponseDTO>
    {
    }
}
