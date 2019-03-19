using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PatchOrderedProduct
{
    public class PatchOrderedProductCommandResponse : CommandResponse<Dictionary<string, object>, PatchOrderedProductCommandResponseDTO>
    {
    }
}
