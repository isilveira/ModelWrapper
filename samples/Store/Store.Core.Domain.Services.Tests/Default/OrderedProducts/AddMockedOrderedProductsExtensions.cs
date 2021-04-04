using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.OrderedProducts
{
    internal static class AddMockedOrderedProductsExtensions
    {
        private static IQueryable<OrderedProduct> GetOrderedProductsCollection()
        {
            return new List<OrderedProduct> {
                new OrderedProduct { Id = 1 },
                new OrderedProduct { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<OrderedProduct>> GetMockedDbSetOrderedProducts()
        {
            var collection = GetOrderedProductsCollection();

            var mockedDbSetOrderedProducts = collection.MockDbSet();

            return mockedDbSetOrderedProducts;
        }

        internal static Mock<IDefaultDbContext> AddMockedOrderedProducts(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetOrderedProducts = GetMockedDbSetOrderedProducts();

            mockedDbContext
                .Setup(setup => setup.OrderedProducts)
                .Returns(mockedDbSetOrderedProducts.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedOrderedProducts(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetOrderedProducts = GetMockedDbSetOrderedProducts();

            mockedDbContextQuery
                .Setup(setup => setup.OrderedProducts)
                .Returns(mockedDbSetOrderedProducts.Object);

            return mockedDbContextQuery;
        }
    }
}
