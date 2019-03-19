using StoreAPI.Core.Application.Interfaces.Responses;

namespace StoreAPI.Core.Application.Bases
{
    public class QueryResponse<TRequest, TDTO> : Response<TRequest, TDTO>, IQueryResponse<TRequest, TDTO>
    {
        public int ResultCount { get; set; }
    }
}
