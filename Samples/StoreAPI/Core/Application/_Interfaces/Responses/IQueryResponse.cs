namespace StoreAPI.Core.Application.Interfaces.Responses
{
    public interface IQueryResponse<TRequest, TDTO> : IResponse<TRequest, TDTO>
    {
        int ResultCount { get; set; }
    }
}
