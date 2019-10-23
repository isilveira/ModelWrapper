using System.Collections.Generic;

namespace ModelWrapper.Interfaces
{
    /// <summary>
    /// Interface for extend functionalities of WrapRequest
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public interface IWrapRequest<TModel>
        where TModel : class
    {
        /// <summary>
        /// Instance of the Model
        /// </summary>
        TModel Model { get; set; }
        /// <summary>
        /// List of properties received on request
        /// </summary>
        List<WrapRequestProperty> AllProperties { get; set; }
        /// <summary>
        /// List of configurations for the request
        /// </summary>
        Dictionary<string, List<string>> ConfigProperties { get; set; }
        /// <summary>
        /// List of configuration values
        /// </summary>
        Dictionary<string, object> ConfigValues { get; set; }
        /// <summary>
        /// Representation of the request object
        /// </summary>
        Dictionary<string, object> RequestObject { get; set; }
    }
}
