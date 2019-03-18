namespace StoreAPI.Core.Application.Interfaces.Responses
{
    public interface ICommandResponse<TRequest, TDTO> : IResponse<TRequest, TDTO>
    {
        string Message { get; set; }
    }
}
