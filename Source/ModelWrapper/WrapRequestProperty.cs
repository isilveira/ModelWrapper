namespace ModelWrapper
{
    public enum WrapPropertySource
    {
        FromBody,
        FromForm,
        FromQuery,
        FromRoute
    }
    public class WrapRequestProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public WrapPropertySource Source { get; set; }
    }
}
