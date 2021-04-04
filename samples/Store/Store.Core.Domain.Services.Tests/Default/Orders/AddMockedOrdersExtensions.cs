using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.Orders
{
    internal static class AddMockedOrdersExtensions
    {
        private static IQueryable<Order> GetOrdersCollection()
        {
            return new List<Order> {
                new Order { Id = 1 },
                new Order { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<Order>> GetMockedDbSetOrders()
        {
            var collection = GetOrdersCollection();

            var mockedDbSetOrders = collection.MockDbSet();

            return mockedDbSetOrders;
        }

        internal static Mock<IDefaultDbContext> AddMockedOrders(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetOrders = GetMockedDbSetOrders();

            mockedDbContext
                .Setup(setup => setup.Orders)
                .Returns(mockedDbSetOrders.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedOrders(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetOrders = GetMockedDbSetOrders();

            mockedDbContextQuery
                .Setup(setup => setup.Orders)
                .Returns(mockedDbSetOrders.Object);

            return mockedDbContextQuery;
        }
    }
}
