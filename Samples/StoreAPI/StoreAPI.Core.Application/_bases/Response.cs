namespace StoreAPI.Core.Application.Bases
{
    public class Response<TRequest, TDTO>
    {
        public TRequest Request { get; set; }
        public TDTO Data { get; set; }
    }
}
