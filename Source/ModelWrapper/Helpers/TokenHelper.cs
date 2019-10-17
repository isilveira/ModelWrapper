using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Helpers
{
    internal class TokenHelper
    {
        internal static IList<string> GetTokens(
            string query,
            bool queryPhrase
        )
        {
            query = query.ToLower();

            query = ConfigurationService.GetConfiguration().ValidateSupressCharacters(query);

            IList<string> tokens = queryPhrase ? new List<string> { query } : query.Split(" ").ToList();

            tokens = ConfigurationService.GetConfiguration().ValidateToken(tokens);

            tokens = ConfigurationService.GetConfiguration().ValidateSupressTokens(tokens);

            return tokens;
        }
    }
}
