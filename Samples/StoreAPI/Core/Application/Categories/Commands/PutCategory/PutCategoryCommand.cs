using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Commands.PutCategory
{
    public class PutCategoryCommand : Wrap<Category>,IRequest<PutCategoryCommandResponse>
    {
        public PutCategoryCommand()
        {
            KeyProperty(x => x.CategoryID);
            SuppressProperty(x => x.LeafCategories);
            SuppressProperty(x => x.Products);
            SuppressProperty(x => x.RootCategory);
        }
    }
}
