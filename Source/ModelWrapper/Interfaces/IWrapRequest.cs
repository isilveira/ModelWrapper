using System.Collections.Generic;
using System.Reflection;

namespace ModelWrapper.Interfaces
{
    public interface IWrapRequest<TModel>
        where TModel : class
    {
        TModel Model { get; set; }
        List<WrapRequestProperty> AllProperties { get; set; }
        List<ConfigProperties> ConfigProperties { get; set; }
        List<string> SuppliedProperties { get; set; }
        List<string> ResponseProperties { get; set; }
        Dictionary<string, object> RequestObject { get; set; }
    }
}
