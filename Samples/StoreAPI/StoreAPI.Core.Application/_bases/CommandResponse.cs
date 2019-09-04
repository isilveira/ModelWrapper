namespace StoreAPI.Core.Application.Bases
{
    public class CommandResponse<TRequest, TDTO> : Response<TRequest, TDTO>
    {
        public string Message { get; set; }
    }
}
