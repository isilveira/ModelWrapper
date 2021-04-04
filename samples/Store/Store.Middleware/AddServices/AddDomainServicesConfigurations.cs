using Store.Core.Domain.Interfaces.Services.Default.Samples;
using Store.Core.Domain.Services.Default.Samples;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Services.Default.Categories;
using Store.Core.Domain.Interfaces.Services.Default.Categories;
using Store.Core.Domain.Interfaces.Services.Default.Customers;
using Store.Core.Domain.Interfaces.Services.Default.Images;
using Store.Core.Domain.Services.Default.Customers;
using Store.Core.Domain.Services.Default.Images;
using Store.Core.Domain.Interfaces.Services.Default.Orders;
using Store.Core.Domain.Services.Default.Orders;
using Store.Core.Domain.Interfaces.Services.Default.OrderedProducts;
using Store.Core.Domain.Services.Default.OrderedProducts;
using Store.Core.Domain.Interfaces.Services.Default.Products;
using Store.Core.Domain.Services.Default.Products;

namespace Store.Middleware.AddServices
{
    public static class AddDomainServicesConfigurations
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IPutCategoryService, PutCategoryService>();
            services.AddTransient<IPostCategoryService, PostCategoryService>();
            services.AddTransient<IPatchCategoryService, PatchCategoryService>();
            services.AddTransient<IDeleteCategoryService, DeleteCategoryService>();

            services.AddTransient<IPutCustomerService, PutCustomerService>();
            services.AddTransient<IPostCustomerService, PostCustomerService>();
            services.AddTransient<IPatchCustomerService, PatchCustomerService>();
            services.AddTransient<IDeleteCustomerService, DeleteCustomerService>();

            services.AddTransient<IPutImageService, PutImageService>();
            services.AddTransient<IPostImageService, PostImageService>();
            services.AddTransient<IPatchImageService, PatchImageService>();
            services.AddTransient<IDeleteImageService, DeleteImageService>();

            services.AddTransient<IPutOrderService, PutOrderService>();
            services.AddTransient<IPostOrderService, PostOrderService>();
            services.AddTransient<IPatchOrderService, PatchOrderService>();
            services.AddTransient<IDeleteOrderService, DeleteOrderService>();

            services.AddTransient<IPutOrderedProductService, PutOrderedProductService>();
            services.AddTransient<IPostOrderedProductService, PostOrderedProductService>();
            services.AddTransient<IPatchOrderedProductService, PatchOrderedProductService>();
            services.AddTransient<IDeleteOrderedProductService, DeleteOrderedProductService>();

            services.AddTransient<IPutProductService, PutProductService>();
            services.AddTransient<IPostProductService, PostProductService>();
            services.AddTransient<IPatchProductService, PatchProductService>();
            services.AddTransient<IDeleteProductService, DeleteProductService>();

            services.AddTransient<IPutSampleService, PutSampleService>();
            services.AddTransient<IPostSampleService, PostSampleService>();
            services.AddTransient<IPatchSampleService, PatchSampleService>();
            services.AddTransient<IDeleteSampleService, DeleteSampleService>();

            return services;
        }
    }
}
