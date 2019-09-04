using ChanceNET;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreAPI.Core.Application.Interfaces.Infrastructures.Data;
using StoreAPI.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Core.Infrastructures.Data.Seeds
{
    public static class IStoreContextSeedExtensions
    {
        public static async Task SeedContext(this IStoreContext context, IConfiguration configuration)
        {
            var seedsMultiplier = configuration.GetSection("AppSettings").GetValue<int>("SeedsMultiplier");
            if (seedsMultiplier == 0)
            {
                return;
            }

            await SeedCategoryAsync(context, 50 * seedsMultiplier);
            await SeedProductsAsync(context, 1_000 * seedsMultiplier);
            await SeedConstumersAsync(context, 500 * seedsMultiplier);
            await SeedOrders(context);
        }

        private static async Task SeedOrders(IStoreContext context)
        {
            Chance chance = new Chance();

            var totalCustomers = await context.Customers.CountAsync();
            var totalCustomersWithOrders = await context.Customers.Where(x=>x.Orders.Any()).CountAsync();
            var totalCustomersWithoutOrders = await context.Customers.Where(x => !x.Orders.Any()).CountAsync();

            if (totalCustomers / 2 < totalCustomersWithOrders)
            {
                return;
            }

            var custumers = await context.Customers.ToListAsync();

            int saveCounter = 0;
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
                    saveCounter++;
                }

                if (saveCounter >= 1_000)
                {
                    await context.SaveChangesAsync();
                    saveCounter = 0;
                }

                chance = chance.New();
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategoryAsync(IStoreContext context, int inserts)
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

        private static async Task SeedProductsAsync(IStoreContext context, int inserts)
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

                    if (saveCounter == 1_000)
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

        private static async Task SeedConstumersAsync(IStoreContext context, int inserts)
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
                    if (saveCounter == 1_000)
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
