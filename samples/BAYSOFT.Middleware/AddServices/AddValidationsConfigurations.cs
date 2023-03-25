﻿using BAYSOFT.Core.Domain.Default.Specifications.Samples;
using BAYSOFT.Core.Domain.Default.Validations.DomainValidations.Samples;
using BAYSOFT.Core.Domain.Default.Validations.EntityValidations;
using Microsoft.Extensions.DependencyInjection;

namespace BAYSOFT.Middleware.AddServices
{
    public static class AddValidationsConfigurations
    {
        public static IServiceCollection AddSpecifications(this IServiceCollection services)
        {
            services.AddTransient<SampleDescriptionAlreadyExistsSpecification>();

            return services;
        }
        public static IServiceCollection AddEntityValidations(this IServiceCollection services)
        {
            services.AddTransient<SampleValidator>();

            return services;
        }
        public static IServiceCollection AddDomainValidations(this IServiceCollection services)
        {
            services.AddTransient<UpdateSampleSpecificationsValidator>();
            services.AddTransient<CreateSampleSpecificationsValidator>();
            services.AddTransient<DeleteSampleSpecificationsValidator>();

            return services;
        }
    }
}
