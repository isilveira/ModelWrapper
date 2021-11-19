using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper
{
    /// <summary>
    /// Class for configuration services
    /// </summary>
    public class ConfigurationService
    {
        /// <summary>
        /// Singleton instance of ConfigurationService
        /// </summary>
        private static ConfigurationService Instance { get; set; }
        /// <summary>
        /// Minimum configurable size for returned collections
        /// </summary>
        public int MinimumReturnedCollectionSize { get; internal set; }
        /// <summary>
        /// Maximum configurable size for returned collections
        /// </summary>
        public int MaximumReturnedCollectionSize { get; internal set; }
        /// <summary>
        /// Configurable default size for returned collections
        /// </summary>
        public int DefaultReturnedCollectionSize { get; internal set; }
        /// <summary>
        /// Minimum configurable size for query terms
        /// </summary>
        public int? QueryTermsMinimumSize { get; internal set; }
        /// <summary>
        /// Maximum configurable size for query terms
        /// </summary>
        public int? QueryTermsMaximumSize { get; internal set; }
        /// <summary>
        /// Default configuration for load complex properties
        /// </summary>
        public bool ByDefaultLoadComplexProperties { get; set; }
        /// <summary>
        /// Suppressed character configuration
        /// </summary>
        public IList<char> SuppressedCharacters { get; internal set; }
        /// <summary>
        /// Suppressed terms configuration
        /// </summary>
        public IList<string> SuppressedTerms { get; internal set; }
        /// <summary>
        /// Entities base type
        /// </summary>
        public Type EntityBase { get; set; }
        /// <summary>
        /// Configuration services constructor
        /// </summary>
        public ConfigurationService()
        {
            MinimumReturnedCollectionSize = 10;
            MaximumReturnedCollectionSize = 1000;
            DefaultReturnedCollectionSize = 50;
            QueryTermsMinimumSize = 3;
            ByDefaultLoadComplexProperties = false;
            SuppressedCharacters = new List<char> { };
            SuppressedTerms = new List<string> { };
        }
        /// <summary>
        /// Method that returns the configuration services instance
        /// </summary>
        /// <returns>configuration service instance</returns>
        public static ConfigurationService GetConfiguration()
        {
            if (Instance == null)
                Instance = new ConfigurationService();

            return Instance;
        }
        /// <summary>
        /// Method that validate the supplied tokens
        /// </summary>
        /// <param name="terms">Quered tokens</param>
        /// <returns>Valid tokens</returns>
        internal IList<string> ValidateTerms(
            IList<string> terms
        )
        {
            if (terms == null || terms.Count == 0)
                return terms;

            return terms.Where(term =>
                (!QueryTermsMinimumSize.HasValue || term.Length >= QueryTermsMinimumSize)
                && (!QueryTermsMaximumSize.HasValue || term.Length <= QueryTermsMaximumSize)
            ).ToList();
        }
        /// <summary>
        /// Method that validate quered string
        /// </summary>
        /// <param name="query">Quered string</param>
        /// <returns>String without suppressed characters</returns>
        internal string ValidateSupressCharacters(
            string query
        )
        {
            if (SuppressedCharacters == null || SuppressedCharacters.Count == 0)
                return query;

            SuppressedCharacters.ToList().ForEach(character => query = query.Replace(character, ' '));

            return query;
        }
        /// <summary>
        /// Method that validate quered terms
        /// </summary>
        /// <param name="terms">Quered terms</param>
        /// <returns>Tokens without suppressed terms</returns>
        internal IList<string> ValidateSupressTokens(
            IList<string> terms
        )
        {
            if (SuppressedTerms == null || SuppressedTerms.Count == 0)
                return terms;

            if (terms == null || terms.Count == 0)
                return terms;

            SuppressedTerms.ToList().ForEach(term => terms.Remove(term));

            return terms;
        }
    }
}
