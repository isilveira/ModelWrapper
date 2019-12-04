﻿using ModelWrapper.Helpers;
using ModelWrapper.Interfaces;
using ModelWrapper.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ModelWrapper.Extensions.Search
{
    public class Search
    {
        public string Query { get; set; }
        public bool Strict { get; set; }
        public bool Phrase { get; set; }
    }
    /// <summary>
    /// Class that extends search functionality into ModelWrapper
    /// </summary>
    public static class SearchExtensions
    {
        /// <summary>
        /// Method that extends IWrapRequest<T> allowing to get query properties from request
        /// </summary>
        /// <typeparam name="TModel">Generic type of the entity</typeparam>
        /// <param name="source">Self IWrapRequest<T> instance</param>
        /// <returns>Returns a dictionary with properties and values found</returns>
        internal static Search Search<TModel>(
            this IWrapRequest<TModel> source
        ) where TModel : class
        {
            var search = new Search();

            #region GET QUERY PROPERTY
            var queryProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryProperty != null)
            {
                bool changed = false;
                string typedValue = TypesHelper.TryChangeType<string>(queryProperty.Value.ToString(), out changed);
                if (changed)
                {
                    search.Query = typedValue;
                }
                else
                {
                    search.Query = string.Empty;
                }
            }
            else
            {
                search.Query = string.Empty;
            }
            #endregion

            #region GET QUERY STRICT PROPERTY
            var queryStrictProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY_STRICT.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryStrictProperty != null)
            {
                bool changed = false;
                bool typedValue = TypesHelper.TryChangeType<bool>(queryStrictProperty.Value.ToString(), out changed);
                if (changed)
                {
                    search.Strict = typedValue;
                }
                else
                {
                    search.Strict = false;
                }
            }
            else
            {
                search.Strict = false;
            }
            #endregion

            #region GET QUERY PHRASE PROPERTY
            var queryPhraseProperty = source.AllProperties.Where(x =>
                        x.Name.ToLower().Equals(Constants.CONST_QUERY_PHRASE.ToLower())
                        && x.Source == WrapPropertySource.FromQuery
                    ).FirstOrDefault();
            if (queryPhraseProperty != null)
            {
                bool changed = false;
                bool typedValue = TypesHelper.TryChangeType<bool>(queryPhraseProperty.Value.ToString(), out changed);
                if (changed)
                {
                    search.Phrase = typedValue;
                }
                else
                {
                    search.Phrase = false;
                }
            }
            else
            {
                search.Phrase = false;
            }
            #endregion

            source.RequestObject.SetValue(Constants.CONST_SEARCH_PROPERTIES, search);
            
            return search;
        }

        public static Search Search<TModel>(
            this WrapResponse<TModel> source
        ) where TModel : class
        {
            return source.OriginalRequest.Search();
        }

        /// <summary>
        /// Method that extends IQueryable<T> allowing to search query with request properties
        /// </summary>
        /// <typeparam name="TSource">Generic type of the entity</typeparam>
        /// <param name="source">Self IQueryable<T> instance</param>
        /// <param name="request">Self IWrapRequest<T> instance</param>
        /// <returns>Returns IQueryable instance with with the configuration for search</returns>
        public static IQueryable<TSource> Search<TSource>(
            this IQueryable<TSource> source,
            IWrapRequest<TSource> request
        ) where TSource : class
        {
            var search = request.Search();

            var query = search.Query;
            var queryStrict = search.Strict;
            var queryPhrase = search.Phrase;

            if (string.IsNullOrWhiteSpace(query))
                return source;

            var queryTokens = TermHelper.GetTerms(query, queryPhrase);

            query = string.Join("+", queryTokens.ToArray());

            if (queryTokens.Count == 0)
                return source;

            List<string> searchableProperties = new List<string>();

            searchableProperties = typeof(TSource).GetProperties().Where(x =>
                 !request.SuppressedProperties().Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).Select(x => x.Name).ToList();

            var criteriaExp = LambdaHelper.GenerateSearchCriteriaExpression<TSource>(searchableProperties, queryTokens, queryStrict);

            return source.Where(criteriaExp);
        }
    }
}
