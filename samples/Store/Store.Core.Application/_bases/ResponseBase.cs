using ModelWrapper;

namespace Store.Core.Application.Bases
{
    public class ResponseBase<TEntity> : WrapResponse<TEntity>
        where TEntity : class
    {
        public ResponseBase(WrapRequest<TEntity> request, object data, string message = "Successful operation!", long? resultCount = null)
            : base(request, data, message, resultCount)
        {
        }
    }
}
