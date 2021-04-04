using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Store.Core.Domain.Services.Tests.Default.Images
{
    internal static class AddMockedImagesExtensions
    {
        private static IQueryable<Image> GetImagesCollection()
        {
            return new List<Image> {
                new Image { Id = 1 },
                new Image { Id = 2 },
            }.AsQueryable();
        }

        private static Mock<DbSet<Image>> GetMockedDbSetImages()
        {
            var collection = GetImagesCollection();

            var mockedDbSetImages = collection.MockDbSet();

            return mockedDbSetImages;
        }

        internal static Mock<IDefaultDbContext> AddMockedImages(this Mock<IDefaultDbContext> mockedDbContext)
        {
            var mockedDbSetImages = GetMockedDbSetImages();

            mockedDbContext
                .Setup(setup => setup.Images)
                .Returns(mockedDbSetImages.Object);

            return mockedDbContext;
        }

        internal static Mock<IDefaultDbContextQuery> AddMockedImages(this Mock<IDefaultDbContextQuery> mockedDbContextQuery)
        {
            var mockedDbSetImages = GetMockedDbSetImages();

            mockedDbContextQuery
                .Setup(setup => setup.Images)
                .Returns(mockedDbSetImages.Object);

            return mockedDbContextQuery;
        }
    }
}
