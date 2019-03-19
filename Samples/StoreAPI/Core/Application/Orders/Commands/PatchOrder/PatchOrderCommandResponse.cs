using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Orders.Commands.PatchOrder
{
    public class PatchOrderCommandResponse : CommandResponse<Dictionary<string, object>, PatchOrderCommandResponseDTO>
    {
    }
}
