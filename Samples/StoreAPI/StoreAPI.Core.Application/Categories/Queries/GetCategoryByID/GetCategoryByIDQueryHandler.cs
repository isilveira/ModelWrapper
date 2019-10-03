using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreAPI.Core.Application.Categories.Queries.GetCategoryByID
{
    public class GetCategoryByIDQueryHandler : IRequestHandler<GetCategoryByIDQuery, GetCategoryByIDQueryResponse>
    {
        private IStoreContext Context { get; set; }
        public GetCategoryByIDQueryHandler(IStoreContext context)
        {
            Context = context;
        }
        public async Task<GetCategoryByIDQueryResponse> Handle(GetCategoryByIDQuery request, CancellationToken cancellationToken)
        {
            var id = request.Project(x => x.CategoryID);

            var data = await Context.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.CategoryID == id);

            if (data == null)
            {
                throw new Exception("Category not found!");
            }

            return new GetCategoryByIDQueryResponse(request, data, "Successful operation!", 1);
        }
    }
}
