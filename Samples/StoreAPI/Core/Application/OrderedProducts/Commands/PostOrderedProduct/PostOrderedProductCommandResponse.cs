using StoreAPI.Core.Application.Bases;
using System.Collections.Generic;

namespace StoreAPI.Core.Application.OrderedProducts.Commands.PostOrderedProduct
{
    public class PostOrderedProductCommandResponse : CommandResponse<Dictionary<string, object>, PostOrderedProductCommandResponseDTO>
    {
    }
}
