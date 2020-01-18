using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for term handling
    /// </summary>
    internal class TermHelper
    {
        /// <summary>
        /// Method that breaks a query string into terms, validating and suppressing as configured.
        /// </summary>
        /// <param name="query">String were terms will be extracted</param>
        /// <param name="queryPhrase">Indication if string must be broken in terms</param>
        /// <returns>List of valid terms</returns>
        internal static IList<string> GetTerms(
            string query,
            bool queryPhrase
        )
        {
            query = query.ToLower();

            query = ConfigurationService.GetConfiguration().ValidateSupressCharacters(query);

            IList<string> tokens = queryPhrase ? new List<string> { query } : query.Split(' ').ToList();

            tokens = ConfigurationService.GetConfiguration().ValidateTerms(tokens);

            tokens = ConfigurationService.GetConfiguration().ValidateSupressTokens(tokens);

            return tokens;
        }
    }
}