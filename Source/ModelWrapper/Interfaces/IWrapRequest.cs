using System.Collections.Generic;
using System.Reflection;

namespace ModelWrapper.Interfaces
{
    public interface IWrapRequest<TModel>
        where TModel : class
    {
        TModel Model { get; set; }
        List<WrapRequestProperty> AllProperties { get; set; }
        Dictionary<string, List<string>> ConfigProperties { get; set; }
        Dictionary<string, object> RequestObject { get; set; }
    }
}
