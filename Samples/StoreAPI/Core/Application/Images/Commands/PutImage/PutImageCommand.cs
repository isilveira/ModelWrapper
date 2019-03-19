using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Commands.PutImage
{
    public class PutImageCommand : Wrap<Image, int>, IRequest<PutImageCommandResponse>
    {
        public PutImageCommand()
        {
            SuppressProperty(x => x.ImageID);
        }
    }
}
