using StoreAPI.Core.Application.Interfaces.Responses;

namespace StoreAPI.Core.Application.Bases
{
    public class CommandResponse<TRequest, TDTO> : Response<TRequest, TDTO>, ICommandResponse<TRequest, TDTO>
    {
        public string Message { get; set; }
    }
}
