﻿using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Store.Middleware.AddServices
{
    public static class AddDbContextConfigurations
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration, Assembly presentationAssembly)
        {
            services.AddDbContext<IDefaultDbContext, DefaultDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(presentationAssembly.GetName().Name)));

            services.AddDbContext<IDefaultDbContextQuery, DefaultDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(presentationAssembly.GetName().Name)));

            return services;
        }
    }
}
