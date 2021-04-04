using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.Customers
{
    internal static class AddMockedCustomersExtensions
    {
        private static IQueryable<Customer> GetCustomersCollection()
        {
            return new List<Customer> {
                new Customer { Id = 1 },
                new Customer { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<Customer>> GetMockedDbSetCustomers()
        {
            var collection = GetCustomersCollection();

            var mockedDbSetCustomers = collection.MockDbSet();

            return mockedDbSetCustomers;
        }

        internal static Mock<IDefaultDbContext> AddMockedCustomers(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetCustomers = GetMockedDbSetCustomers();

            mockedDbContext
                .Setup(setup => setup.Customers)
                .Returns(mockedDbSetCustomers.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedCustomers(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetCustomers = GetMockedDbSetCustomers();

            mockedDbContextQuery
                .Setup(setup => setup.Customers)
                .Returns(mockedDbSetCustomers.Object);

            return mockedDbContextQuery;
        }
    }
}
