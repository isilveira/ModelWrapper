using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.Products.Commands.PatchProduct
{
    public class PatchProductCommandResponse : CommandResponse<Dictionary<string, object>, PatchProductCommandResponseDTO>
    {
    }
}
