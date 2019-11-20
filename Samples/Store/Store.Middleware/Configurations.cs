using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelWrapper.Middleware;
using Store.Core.Application.Interfaces.Infrastructures.Data;
using Store.Core.Infrastructures.Data;
using Store.Core.Infrastructures.Data.Seeds;
using System;

namespace Store.Middleware
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
            var assembly = AppDomain.CurrentDomain.Load("Store.Core.Application");

            services.AddMediatR(assembly);

            services.AddModelWrapper()
                .AddDefaultReturnedCollectionSize(10)
                .AddMinimumReturnedCollectionSize(1)
                .AddMaximumReturnedCollectionSize(100)
                .AddQueryTermsMinimumSize(3)
                .AddSuppressedTerms(new string[] { "the" });

            return services;
        }

        public static IApplicationBuilder Configure(this IApplicationBuilder app, IConfiguration configuration)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using(var context = serviceScope.ServiceProvider.GetService<IStoreContext>())
                {
                    context.SeedContext(configuration).Wait();
                }
            }

            return app;
        }
    }
}
