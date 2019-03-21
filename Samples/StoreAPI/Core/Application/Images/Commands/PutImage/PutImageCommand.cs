using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommand : Wrap<Image>, IRequest<PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            KeyProperty(x => x.ImageID);
            SuppressProperty(x => x.Product);
        }
    }
}
