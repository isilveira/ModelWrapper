using MediatR;
using ModelWrapper;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQuery : WrapRequest<Category>, IRequest<GetCategoriesByFilterQueryResponse>
    {
        public GetCategoriesByFilterQuery()
        {
            ConfigKeys(x => x.CategoryID);
            ConfigSuppressedProperties(x => x.LeafCategories);
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.Products);
            //OrderBy = "CategoryID";
            //SetRestrictProperty(x => x.LeafCategories);
            //SetRestrictProperty(x => x.RootCategory);
            //SetRestrictProperty(x => x.Products);
        }
    }
}
