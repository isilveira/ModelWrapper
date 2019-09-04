namespace StoreAPI.Core.Application.Bases
{
    public class QueryResponse<TRequest, TDTO> : Response<TRequest, TDTO>
    {
        public int ResultCount { get; set; }
    }
}
