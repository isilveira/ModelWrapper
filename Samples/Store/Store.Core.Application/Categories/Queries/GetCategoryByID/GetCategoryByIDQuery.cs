﻿using MediatR;
using ModelWrapper;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQuery : WrapRequest<Category>, IRequest<GetCategoryByIDQueryResponse>
    {
        public GetCategoryByIDQuery()
        {
            ConfigKeys(x => x.CategoryID);
            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
            ConfigSuppressedResponseProperties(x => x.LeafCategories);
            ConfigSuppressedResponseProperties(x => x.RootCategory);
            ConfigSuppressedResponseProperties(x => x.Products);
        }
    }
}