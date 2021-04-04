using Store.Core.Domain.Validations.DomainValidations.Default.Samples;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Store.Core.Domain.Validations.Specifications.Default.Samples;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;

namespace Store.Middleware.AddServices
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
            services.AddTransient<CategoryValidator>();
            services.AddTransient<CustomerValidator>();
            services.AddTransient<ImageValidator>();
            services.AddTransient<OrderValidator>();
            services.AddTransient<OrderedProductValidator>();
            services.AddTransient<ProductValidator>();
            services.AddTransient<SampleValidator>();

            return services;
        }
        public static IServiceCollection AddDomainValidations(this IServiceCollection services)
        {
            services.AddTransient<PutCategorySpecificationsValidator>();
            services.AddTransient<PostCategorySpecificationsValidator>();
            services.AddTransient<PatchCategorySpecificationsValidator>();
            services.AddTransient<DeleteCategorySpecificationsValidator>();

            services.AddTransient<PutCustomerSpecificationsValidator>();
            services.AddTransient<PostCustomerSpecificationsValidator>();
            services.AddTransient<PatchCustomerSpecificationsValidator>();
            services.AddTransient<DeleteCustomerSpecificationsValidator>();

            services.AddTransient<PutImageSpecificationsValidator>();
            services.AddTransient<PostImageSpecificationsValidator>();
            services.AddTransient<PatchImageSpecificationsValidator>();
            services.AddTransient<DeleteImageSpecificationsValidator>();

            services.AddTransient<PutOrderSpecificationsValidator>();
            services.AddTransient<PostOrderSpecificationsValidator>();
            services.AddTransient<PatchOrderSpecificationsValidator>();
            services.AddTransient<DeleteOrderSpecificationsValidator>();

            services.AddTransient<PutOrderedProductSpecificationsValidator>();
            services.AddTransient<PostOrderedProductSpecificationsValidator>();
            services.AddTransient<PatchOrderedProductSpecificationsValidator>();
            services.AddTransient<DeleteOrderedProductSpecificationsValidator>();

            services.AddTransient<PutProductSpecificationsValidator>();
            services.AddTransient<PostProductSpecificationsValidator>();
            services.AddTransient<PatchProductSpecificationsValidator>();
            services.AddTransient<DeleteProductSpecificationsValidator>();

            services.AddTransient<PutSampleSpecificationsValidator>();
            services.AddTransient<PostSampleSpecificationsValidator>();
            services.AddTransient<PatchSampleSpecificationsValidator>();
            services.AddTransient<DeleteSampleSpecificationsValidator>();

            return services;
        }
    }
}
