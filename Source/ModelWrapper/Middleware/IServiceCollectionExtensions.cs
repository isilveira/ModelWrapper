using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Middleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddModelWrapper(this IServiceCollection services)
        {
            Configuration.GetConfiguration();

            services.AddSingleton<Configuration>();

            return services;
        }
        public static IServiceCollection AddQueryTokenMinimumSize(this IServiceCollection services, int? minimumSize)
        {
            Configuration.GetConfiguration().QueryTokenMinimumSize = minimumSize;

            return services;
        }
        public static IServiceCollection AddQueryTokenMaximumSize(this IServiceCollection services, int? maximumSize)
        {
            Configuration.GetConfiguration().QueryTokenMaximumSize = maximumSize;

            return services;
        }

        public static IServiceCollection AddDefaultReturnedCollectionSize(this IServiceCollection services, int defaultReturnedCollectionSize)
        {
            Configuration.GetConfiguration().DefaultReturnedCollectionSize = defaultReturnedCollectionSize;

            return services;
        }

        public static IServiceCollection AddMaximumReturnedCollectionSize(this IServiceCollection services, int maximumReturnedCollectionSize)
        {
            Configuration.GetConfiguration().MaximumReturnedCollectionSize = maximumReturnedCollectionSize;

            return services;
        }

        public static IServiceCollection AddMinimumReturnedCollectionSize(this IServiceCollection services, int minimumReturnedCollectionSize)
        {
            Configuration.GetConfiguration().MinimumReturnedCollectionSize = minimumReturnedCollectionSize;

            return services;
        }
        public static IServiceCollection AddSuppressedCharacters(this IServiceCollection services, IList<char> suppressedCharacters = null)
        {
            Configuration.GetConfiguration().SuppressedCharacters = suppressedCharacters;

            return services;
        }
        public static IServiceCollection AddSuppressedTokens(this IServiceCollection services, IList<string> suppressedTokens = null)
        {
            Configuration.GetConfiguration().SuppressedTokens = suppressedTokens;

            return services;
        }
    }
}
