using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PutOrderedProduct
{
    public class PutOrderedProductCommandResponse : CommandResponse<Dictionary<string, object>, PutOrderedProductCommandResponseDTO>
    {
    }
}
