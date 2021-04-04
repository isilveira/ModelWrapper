using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.Categories
{
    internal static class AddMockedCategoriesExtensions
    {
        private static IQueryable<Category> GetCategoriesCollection()
        {
            return new List<Category> {
                new Category { Id = 1 },
                new Category { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<Category>> GetMockedDbSetCategories()
        {
            var collection = GetCategoriesCollection();

            var mockedDbSetCategories = collection.MockDbSet();

            return mockedDbSetCategories;
        }

        internal static Mock<IDefaultDbContext> AddMockedCategories(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetCategories = GetMockedDbSetCategories();

            mockedDbContext
                .Setup(setup => setup.Categories)
                .Returns(mockedDbSetCategories.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedCategories(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetCategories = GetMockedDbSetCategories();

            mockedDbContextQuery
                .Setup(setup => setup.Categories)
                .Returns(mockedDbSetCategories.Object);

            return mockedDbContextQuery;
        }
    }
}
