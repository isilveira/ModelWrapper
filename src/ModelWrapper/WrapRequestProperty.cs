namespace ModelWrapper
{
    /// <summary>
    /// Data Source Enumerator
    /// </summary>
    public enum WrapPropertySource
    {
        FromBody,
        FromForm,
        FromQuery,
        FromRoute
    }
    /// <summary>
    /// Class representing data arriving at the endpoint
    /// </summary>
    public class WrapRequestProperty
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Property value
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Property source
        /// </summary>
        public WrapPropertySource Source { get; set; }
    }
}
