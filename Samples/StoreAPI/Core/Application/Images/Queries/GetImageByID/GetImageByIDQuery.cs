using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Images.Queries.GetImageByID
{
    public class GetImageByIDQuery : IRequest<GetImageByIDQueryResponse>
    {
        public int ImageID { get; set; }
        public GetImageByIDQuery()
        {
        }
    }
}
