using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Middleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddModelWrapper(
            this IServiceCollection services
        )
        {
            ConfigurationService.GetConfiguration();

            services.AddSingleton<ConfigurationService>();

            return services;
        }
        public static IServiceCollection AddQueryTokenMinimumSize(
            this IServiceCollection services,
            int? minimumSize
        )
        {
            ConfigurationService.GetConfiguration().QueryTokenMinimumSize = minimumSize;

            return services;
        }
        public static IServiceCollection AddQueryTokenMaximumSize(
            this IServiceCollection services,
            int? maximumSize
        )
        {
            ConfigurationService.GetConfiguration().QueryTokenMaximumSize = maximumSize;

            return services;
        }

        public static IServiceCollection AddDefaultReturnedCollectionSize(
            this IServiceCollection services,
            int defaultReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().DefaultReturnedCollectionSize = defaultReturnedCollectionSize;

            return services;
        }

        public static IServiceCollection AddMaximumReturnedCollectionSize(
            this IServiceCollection services,
            int maximumReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().MaximumReturnedCollectionSize = maximumReturnedCollectionSize;

            return services;
        }

        public static IServiceCollection AddMinimumReturnedCollectionSize(
            this IServiceCollection services,
            int minimumReturnedCollectionSize
        )
        {
            ConfigurationService.GetConfiguration().MinimumReturnedCollectionSize = minimumReturnedCollectionSize;

            return services;
        }
        public static IServiceCollection AddSuppressedCharacters(
            this IServiceCollection services,
            IList<char> suppressedCharacters = null
        )
        {
            ConfigurationService.GetConfiguration().SuppressedCharacters = suppressedCharacters;

            return services;
        }
        public static IServiceCollection AddSuppressedTokens(
            this IServiceCollection services,
            IList<string> suppressedTokens = null
        )
        {
            ConfigurationService.GetConfiguration().SuppressedTokens = suppressedTokens;

            return services;
        }
    }
}
