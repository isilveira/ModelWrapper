using Store.Core.Application.Bases;
using Store.Core.Domain.Entities;

namespace Store.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQuery : RequestBase<Category, GetCategoriesByFilterQueryResponse>
    {
        public GetCategoriesByFilterQuery()
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
