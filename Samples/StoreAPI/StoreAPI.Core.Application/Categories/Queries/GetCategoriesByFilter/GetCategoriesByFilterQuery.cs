using EntitySearch;
using MediatR;
using StoreAPI.Core.Domain.Entities;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQuery : EntitySearch<Category>, IRequest<GetCategoriesByFilterQueryResponse>
    {
        public GetCategoriesByFilterQuery()
        {
            OrderBy = "CategoryID";
            SetRestrictProperty(x => x.LeafCategories);
            SetRestrictProperty(x => x.RootCategory);
            SetRestrictProperty(x => x.Products);
        }
    }
}
