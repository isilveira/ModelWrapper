using MediatR;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoriesByFilter
{
    public class GetCategoriesByFilterQuery : IRequest<GetCategoriesByFilterQueryResponse>
    {
        public int? RootCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GetCategoriesByFilterQuery()
        {
        }
    }
}
