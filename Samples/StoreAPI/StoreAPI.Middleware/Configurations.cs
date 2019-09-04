using EntitySearch;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Infrastructures.Data;
using System;

namespace StoreAPI.Middleware
{
    public static class Configurations
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IStoreContext, StoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("StoreAPI.Core.Application");

            services.AddMediatR(assembly);

            services.AddEntitySearch()
                .SetTokenMinimumSize(3)
                .SetSupressTokens(new string[] { "the" });

            return services;
        }
    }
}
