using StoreAPI.Core.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommandResponse : CommandResponse<Dictionary<string, object>, PutImageCommandResponseDTO>
    {
    }
}
