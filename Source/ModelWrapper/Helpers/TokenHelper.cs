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

            query = Configuration.GetConfiguration().ValidateSupressCharacters(query);

            IList<string> tokens = queryPhrase ? new List<string> { query } : query.Split(" ").ToList();

            tokens = Configuration.GetConfiguration().ValidateToken(tokens);

            tokens = Configuration.GetConfiguration().ValidateSupressTokens(tokens);

            return tokens;
        }
    }
}
