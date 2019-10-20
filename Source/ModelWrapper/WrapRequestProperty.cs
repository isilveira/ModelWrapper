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
        public string Name { get; set; }
        public object Value { get; set; }
        public WrapPropertySource Source { get; set; }
    }
}
