using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommand : IRequest<PostCategoryCommandResponse>
    {
        public int? RootCategoryID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
