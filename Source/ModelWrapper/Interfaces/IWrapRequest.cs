using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ModelWrapper.Interfaces
{
    public interface IWrapRequest<TModel>
        where TModel : class
    {
        TModel Model { get; set; }
        IList<NewWrapProperty> AllProperties { get; set; }
        IList<PropertyInfo> SuppliedProperties { get; set; }
        Dictionary<string, object> RequestObject { get; set; }
    }
}
