namespace StoreAPI.Core.Application.Interfaces.Responses
{
    public interface IResponse<TRequest, TDTO>
    {
        TRequest Request { get; set; }
        TDTO Data { get; set; }
    }
}
