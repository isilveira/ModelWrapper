using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for term handling
    /// </summary>
    internal class TermsHelper
    {
        /// <summary>
        /// Method that breaks a query string into terms, validating and suppressing as configured.
        /// </summary>
        /// <param name="query">String were terms will be extracted</param>
        /// <param name="queryPhrase">Indication if string must be broken in terms</param>
        /// <returns>List of valid terms</returns>
        internal static IList<string> GetSearchTerms(
            string query,
            bool queryPhrase
        )
        {
            query = query.ToLower();

            query = ConfigurationService.GetConfiguration().ValidateSupressCharacters(query);

            IList<string> tokens = queryPhrase ? new List<string> { query } : GetTerms(query, " ");

            tokens = ConfigurationService.GetConfiguration().ValidateTerms(tokens);

            tokens = ConfigurationService.GetConfiguration().ValidateSupressTokens(tokens);

            return tokens;
        }
        internal static IList<string> GetTerms(
            string text,
            string separator)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            return text.Split(separator).ToList();
        }
    }
}