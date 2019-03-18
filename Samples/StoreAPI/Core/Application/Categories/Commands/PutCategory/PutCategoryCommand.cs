using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PutCategory
{
    public class PutCategoryCommand : IRequest<PutCategoryCommandResponse>
    {
        public int CategoryID { get; set; }
        public int? RootCategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
