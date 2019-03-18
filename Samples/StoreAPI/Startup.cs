using ChanceNET;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreAPI.Core.Application.Interfaces.Contexts;
using StoreAPI.Core.Domain.Entities;
using StoreAPI.Core.Infrastructures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("StoreAPI");

            services.AddMediatR(assembly);

            services.AddDbContext<IStoreContext, StoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStoreContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SeedStoreContextAsync(context).Wait();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private async Task SeedStoreContextAsync(IStoreContext context)
        {
            await SeedCategoryAsync(context, 5000);
            await SeedProductsAsync(context, 100_000);
            await SeedConstumersAsync(context, 50_000);
            await SeedOrders(context);
        }

        private async Task SeedOrders(IStoreContext context)
        {
            Chance chance = new Chance();

            if (await context.Orders.CountAsync() > 0)
            {
                return;
            }

            var custumers = await context.Customers.ToListAsync();

            foreach (var customer in custumers)
            {
                if (chance.Bool())
                {
                    int productCount = chance.Integer(1, 4);
                    var date = chance.Date(0, 0, 0, customer.RegistrationDate.Year + 1, 2019);
                    var confirmed = chance.Bool();
                    var order = new Order
                    {
                        RegistrationDate = date,
                        ConfirmationDate = confirmed ? date : default(DateTime?),
                        CancellationDate = !confirmed ? date : default(DateTime?),
                        OrderedProducts = new List<OrderedProduct>()
                    };
                    for (int i = 0; i < productCount; i++)
                    {
                        var productMaxID = context.Products.Max(x => x.ProductID);
                        var productID = chance.Integer(1, productMaxID);
                        var product = await context.Products.SingleOrDefaultAsync(x => x.ProductID == productID);

                        while (product == null)
                        {
                            productID = chance.Integer(1, productMaxID);
                            product = await context.Products.SingleOrDefaultAsync(x => x.ProductID == productID);
                        }

                        var orderedProduct = new OrderedProduct
                        {
                            Amount = chance.Integer(1, 4),
                            Value = product.Value,
                            ProductID = product.ProductID,
                            RegistrationDate = date
                        };

                        order.OrderedProducts.Add(orderedProduct);
                    }
                    if (customer.Orders == null)
                    {
                        customer.Orders = new List<Order>();
                    }
                    customer.Orders.Add(order);
                }
                chance = chance.New();
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedCategoryAsync(IStoreContext context, int inserts)
        {
            Chance chance = new Chance();
            int categoriesCount = context.Categories.Count();
            while (categoriesCount < inserts)
            {
                for (int i = categoriesCount; i < inserts; i++)
                {
                    var category = new Category
                    {
                        Name = chance.Word(0, 0, true),
                        Description = chance.Paragraph(3),
                        RootCategoryID = chance.Bool() && i > 0 ? chance.Integer(1, i) : default(int?)
                    };

                    await context.Categories.AddAsync(category);

                    await context.SaveChangesAsync();

                    chance = chance.New();
                }

                categoriesCount = context.Categories.Count();
            }
        }

        private async Task SeedProductsAsync(IStoreContext context, int inserts)
        {
            Chance chance = new Chance();
            int productsCount = context.Products.Count();
            while (productsCount < inserts)
            {
                int saveCounter = 0;
                for (int i = productsCount; i < inserts; i++)
                {
                    var product = new Product
                    {
                        Name = chance.Sentence(6, 0, true, true, '.'),
                        Description = chance.Paragraph(3),
                        Specifications = chance.Paragraph(6),
                        Amount = chance.Integer(0, 500),
                        Value = Convert.ToDecimal(chance.Double(0, 250)),
                        RegistrationDate = chance.Date(0, 0, 0, 2010, 2018),
                        IsVisible = chance.Bool(),
                        CategoryID = chance.Integer(1, 50)
                    };

                    if (product.Images == null)
                    {
                        product.Images = new List<Image>();
                    }

                    for (int x = 0; x <= chance.Integer(1, 5); x++)
                    {
                        product.Images.Add(new Image { MimeType = "image/jpg", Url = "https://picsum.photos/500/500?image=" + chance.Integer(0, 1000).ToString() });
                    }

                    await context.Products.AddAsync(product);
                    saveCounter++;

                    if (saveCounter == 10000)
                    {
                        await context.SaveChangesAsync();
                        saveCounter = 0;
                    }

                    chance = chance.New();
                }
                if (saveCounter > 0)
                {
                    await context.SaveChangesAsync();
                }

                productsCount = context.Products.Count();
            }
        }

        private async Task SeedConstumersAsync(IStoreContext context, int inserts)
        {
            Chance chance = new Chance();
            int customersCount = context.Customers.Count();
            while (customersCount < inserts)
            {
                int saveCounter = 0;
                for (int i = customersCount; i < inserts; i++)
                {
                    var customer = new Customer
                    {
                        Name = chance.FullName(),
                        Email = chance.Email(),
                        RegistrationDate = chance.Date(0, 0, 0, 2010, 2018)
                    };

                    await context.Customers.AddAsync(customer);
                    saveCounter++;
                    if (saveCounter == 10000)
                    {
                        await context.SaveChangesAsync();
                        saveCounter = 0;
                    }

                    chance = chance.New();
                }

                if (saveCounter > 0)
                {
                    await context.SaveChangesAsync();
                }

                customersCount = context.Customers.Count();
            }
        }
    }
}
