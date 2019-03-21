using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PostCategory
{
    public class PostCategoryCommand : Wrap<Category>, IRequest<PostCategoryCommandResponse>
    {
        public PostCategoryCommand()
        {
            KeyProperty(x => x.CategoryID);
            SuppressProperty(x => x.LeafCategories);
            SuppressProperty(x => x.Products);
            SuppressProperty(x => x.RootCategory);
        }
    }
}
