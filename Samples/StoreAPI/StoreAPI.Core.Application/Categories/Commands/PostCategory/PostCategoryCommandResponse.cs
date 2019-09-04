using StoreAPI.Core.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommandResponse : CommandResponse<Dictionary<string, object>, PostCategoryCommandResponseDTO>
    {
    }
}
