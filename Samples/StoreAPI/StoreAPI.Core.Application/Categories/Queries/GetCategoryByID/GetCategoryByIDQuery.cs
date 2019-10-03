using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQuery : WrapRequest<Category>, IRequest<GetCategoryByIDQueryResponse>
    {
        public GetCategoryByIDQuery()
        {
            ConfigKeys(x => x.CategoryID);
            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
        }
    }
}
