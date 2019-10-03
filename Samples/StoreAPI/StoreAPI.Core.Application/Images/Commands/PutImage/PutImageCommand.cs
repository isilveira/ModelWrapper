using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommand : WrapRequest<Image>, IRequest<PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
