using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Categories
{

    [TestClass]
    public class PutCategoryServiceTest
    {
        private PutCategoryService GetMockedPutCategoryService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedCategoryValidator = new CategoryValidator();

            var mockedPutCategorySpecificationsValidator = new PutCategorySpecificationsValidator();

            var mockedPutCategoryService = new PutCategoryService(
                mockedDbContext.Object,
                mockedCategoryValidator,
                mockedPutCategorySpecificationsValidator);

            return mockedPutCategoryService;
        }

        [TestMethod]
        public async Task TestPutCategoryValidModelAsync()
        {
            var mockedPutCategoryService = GetMockedPutCategoryService();

            var mockedCategory = new Category
            {
                Id = 1,
            };

            await mockedPutCategoryService.Run(mockedCategory);
        }
    }
}
