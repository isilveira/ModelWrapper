using StoreAPI.Core.Application.Interfaces.Responses;

namespace StoreAPI.Core.Application.Bases
{
    public class Response<TRequest, TDTO> : IResponse<TRequest, TDTO>
    {
        public TRequest Request { get; set; }
        public TDTO Data { get; set; }
    }
}
