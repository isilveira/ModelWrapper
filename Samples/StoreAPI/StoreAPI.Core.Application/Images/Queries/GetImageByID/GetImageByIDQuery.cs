using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQuery : WrapRequest<Image>, IRequest<GetImageByIDQueryResponse>
    {
        public GetImageByIDQuery()
        {
            ConfigKeys(x => x.ImageID);
            ConfigSuppressedProperties(x => x.Product);
        }
    }
}
