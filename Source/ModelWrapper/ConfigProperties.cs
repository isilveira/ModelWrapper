using System.Collections.Generic;

namespace ModelWrapper
{
    /// <summary>
    /// Class that represents configuration properties
    /// </summary>
    public class ConfigProperties
    {
        /// <summary>
        /// Configuration list name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of propeties names
        /// </summary>
        public List<string> Properties { get; set; }
    }
}
