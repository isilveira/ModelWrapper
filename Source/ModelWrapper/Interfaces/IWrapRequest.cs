using System.Collections.Generic;
using System.Reflection;

namespace ModelWrapper.Interfaces
{
    public interface IWrapRequest<TModel>
        where TModel : class
    {
        TModel Model { get; set; }
        List<NewWrapProperty> AllProperties { get; set; }
        List<PropertyInfo> KeyProperties { get; set; }
        List<PropertyInfo> SupressedProperties { get; set; }
        List<PropertyInfo> SupressedResponseProperties { get; set; }
        List<PropertyInfo> SuppliedProperties { get; set; }
        List<PropertyInfo> ResponseProperties { get; set; }
        Dictionary<string, object> RequestObject { get; set; }
    }
}
