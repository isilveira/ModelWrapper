using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ModelWrapper.Middleware
{
    /// <summary>
    /// Class that extends IServiceCollection
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Method that adds ModelWrapper configuration into services as singleton
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddModelWrapper(
            this IServiceCollection services
        )
        {
            ConfigurationService.GetConfiguration();

            services.AddSingleton<ConfigurationService>();

            return services;
        }
        /// <summary>
        /// Method that configures the minimum size for query terms
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="minimumSize">Minimum size</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddQueryTermsMinimumSize(
            this IServiceCollection services,
            int? minimumSize
        )
        {
            ConfigurationService.GetConfiguration().QueryTermsMinimumSize = minimumSize;

            return services;
        }
        /// <summary>
        /// Method that configures the maximum size for query terms
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="maximumSize">Maximum size</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddQueryTermsMaximumSize(
            this IServiceCollection services,
            int? maximumSize
        )
        {
            ConfigurationService.GetConfiguration().QueryTermsMaximumSize = maximumSize;

            return services;
        }
        /// <summary>
        /// Method that configures the default size for returned collections
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="defaultReturnedCollectionSize">Default size for returned collections</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddDefaultReturnedCollectionSize(
            this IServiceCollection services,
            int defaultReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().DefaultReturnedCollectionSize = defaultReturnedCollectionSize;

            return services;
        }
        /// <summary>
        /// Method that configures the maximum size for returned collections
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="maximumReturnedCollectionSize">Maximum size for returned collections</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddMaximumReturnedCollectionSize(
            this IServiceCollection services,
            int maximumReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().MaximumReturnedCollectionSize = maximumReturnedCollectionSize;

            return services;
        }
        /// <summary>
        /// Method that configures the minimum size for returned collections
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="minimumReturnedCollectionSize">Minimum size for returned collections</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddMinimumReturnedCollectionSize(
            this IServiceCollection services,
            int minimumReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().MinimumReturnedCollectionSize = minimumReturnedCollectionSize;

            return services;
        }
        /// <summary>
        /// Method tahe configures the default load behavior for complex properties
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="byDefaultLoadComplexProperties">Default load behavior for complex properties. If not set default is false.</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddByDefaultLoadComplexProperties(
            this IServiceCollection services,
            bool byDefaultLoadComplexProperties)
        {
            ConfigurationService.GetConfiguration().ByDefaultLoadComplexProperties = byDefaultLoadComplexProperties;

            return services;
        }
        /// <summary>
        /// Method that configures a list of characters that will be ignored on quered strings
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="suppressedCharacters">List of characters to suppress</param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddSuppressedCharacters(
            this IServiceCollection services,
            IList<char> suppressedCharacters = null
        )
        {
            ConfigurationService.GetConfiguration().SuppressedCharacters = suppressedCharacters;

            return services;
        }
        /// <summary>
        /// Method that configures a list of terms that will be ignored on quered strings 
        /// </summary>
        /// <param name="services">Self instance of IServiceCollection</param>
        /// <param name="suppressedTerms"></param>
        /// <returns>Instance of IServiceCollection</returns>
        public static IServiceCollection AddSuppressedTerms(
            this IServiceCollection services,
            IList<string> suppressedTerms = null
        )
        {
            ConfigurationService.GetConfiguration().SuppressedTerms = suppressedTerms;

            return services;
        }
        public static IServiceCollection AddEntityBaseType(
            this IServiceCollection services,
            Type entityBaseType)
        {
            ConfigurationService.GetConfiguration().EntityBase = entityBaseType;

            return services;
		}
		public static IServiceCollection AddByDefaultInStringSeparator(
			this IServiceCollection services,
			string separator)
		{
			ConfigurationService.GetConfiguration().DefaultInStringSeparator = separator;

			return services;
		}
	}
}
