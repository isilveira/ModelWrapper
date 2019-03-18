using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQuery : IRequest<GetCategoryByIDQueryResponse>
    {
        public int CategoryID { get; set; }
        public GetCategoryByIDQuery()
        {
        }
    }
}
