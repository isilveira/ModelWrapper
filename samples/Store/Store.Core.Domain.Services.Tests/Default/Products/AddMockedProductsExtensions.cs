using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.Products
{
    internal static class AddMockedProductsExtensions
    {
        private static IQueryable<Product> GetProductsCollection()
        {
            return new List<Product> {
                new Product { Id = 1 },
                new Product { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<Product>> GetMockedDbSetProducts()
        {
            var collection = GetProductsCollection();

            var mockedDbSetProducts = collection.MockDbSet();

            return mockedDbSetProducts;
        }

        internal static Mock<IDefaultDbContext> AddMockedProducts(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetProducts = GetMockedDbSetProducts();

            mockedDbContext
                .Setup(setup => setup.Products)
                .Returns(mockedDbSetProducts.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedProducts(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetProducts = GetMockedDbSetProducts();

            mockedDbContextQuery
                .Setup(setup => setup.Products)
                .Returns(mockedDbSetProducts.Object);

            return mockedDbContextQuery;
        }
    }
}
