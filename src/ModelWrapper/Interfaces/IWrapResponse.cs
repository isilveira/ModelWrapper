using System.Collections.Generic;

namespace ModelWrapper.Interfaces
{
    public interface IWrapResponse
    {
        long ResultCount { get; set; }
        string Message { get; set; }
        object Request { get; set; }
        object Data { get; set; }
        Dictionary<string, object> Notifications { get; set; }
    }
    /// <summary>
    /// Interface for extend functionalities of WrapResponse
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface IWrapResponse<TModel> : IWrapResponse where TModel : class
    {
    }
}
