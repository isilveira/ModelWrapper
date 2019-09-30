namespace ModelWrapper.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return source;
            if (source.Length == 1)
                return source.ToLower();

            return source.Substring(0, 1).ToLower() + source.Substring(1);
        }
    }
}
