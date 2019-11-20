namespace ModelWrapper.Extensions
{
    /// <summary>
    /// Class that extends functionalities of string instances
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Method that turns the first character into lower case.
        /// </summary>
        /// <param name="source">String instance</param>
        /// <returns>Transformed string</returns>
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
