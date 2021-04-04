using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Categories
{
    [TestClass]
    public class PatchCategoryServiceTest
    {
        private PatchCategoryService GetMockedPatchCategoryService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedCategoryValidator = new CategoryValidator();

            var mockedPatchCategorySpecificationsValidator = new PatchCategorySpecificationsValidator();

            var mockedPatchCategoryService = new PatchCategoryService(
                mockedDbContext.Object,
                mockedCategoryValidator,
                mockedPatchCategorySpecificationsValidator
                );

            return mockedPatchCategoryService;
        }

        [TestMethod]
        public async Task TestPatchCategoryValidModelAsync()
        {
            var mockedPatchCategoryService = GetMockedPatchCategoryService();

            var mockedCategory = new Category
            {
                Id = 1,
            };

            await mockedPatchCategoryService.Run(mockedCategory);
        }
    }
}
