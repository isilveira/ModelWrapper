using Newtonsoft.Json.Linq;

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
            return System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(source);
        }
    }
}
