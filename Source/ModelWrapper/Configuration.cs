using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    public class Configuration
    {
        private static Configuration Instance { get; set; }
        public int MinimumReturnedCollectionSize { get; internal set; }
        public int MaximumReturnedCollectionSize { get; internal set; }
        public int DefaultReturnedCollectionSize { get; internal set; }
        public int? QueryTokenMinimumSize { get; internal set; }
        public int? QueryTokenMaximumSize { get; internal set; }
        public IList<char> SuppressedCharacters { get; internal set; }
        public IList<string> SuppressedTokens { get; internal set; }
        public Configuration()
        {
            MinimumReturnedCollectionSize = 10;
            MaximumReturnedCollectionSize = 1000;
            DefaultReturnedCollectionSize = 50;
            QueryTokenMinimumSize = 3;
            SuppressedCharacters = new List<char> { };
            SuppressedTokens = new List<string> { };
        }

        public static Configuration GetConfiguration()
        {
            if (Instance == null)
                Instance = new Configuration();

            return Instance;
        }

        internal IList<string> ValidateToken(IList<string> tokens)
        {
            if (tokens == null || tokens.Count == 0)
                return tokens;

            return tokens.Where(token =>
                (!QueryTokenMinimumSize.HasValue || token.Length >= QueryTokenMinimumSize)
                && (!QueryTokenMaximumSize.HasValue || token.Length <= QueryTokenMaximumSize)
            ).ToList();
        }
        internal string ValidateSupressCharacters(string query)
        {
            if (SuppressedCharacters == null || SuppressedCharacters.Count == 0)
                return query;

            SuppressedCharacters.ToList().ForEach(character => query = query.Replace(character, ' '));

            return query;
        }
        internal IList<string> ValidateSupressTokens(IList<string> tokens)
        {
            if (SuppressedTokens == null || SuppressedTokens.Count == 0)
                return tokens;

            if (tokens == null || tokens.Count == 0)
                return tokens;

            SuppressedTokens.ToList().ForEach(token => tokens.Remove(token));

            return tokens;
        }
    }
}
