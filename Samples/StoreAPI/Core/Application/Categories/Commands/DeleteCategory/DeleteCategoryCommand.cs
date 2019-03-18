using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int CategoryID { get; set; }
    }
}
