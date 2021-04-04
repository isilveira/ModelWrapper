using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Categories
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private PostCategoryService GetMockedPostCategoryService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedCategoryValidator = new CategoryValidator();

            var mockedPostCategorySpecificationsValidator = new PostCategorySpecificationsValidator();

            var mockedPostCategoryService = new PostCategoryService(
                mockedDbContext.Object,
                mockedCategoryValidator,
                mockedPostCategorySpecificationsValidator);

            return mockedPostCategoryService;
        }

        [TestMethod]
        public async Task TestPostCategoryValidModelAsync()
        {
            var mockedPostCategoryService = GetMockedPostCategoryService();

            var mockedCategory = new Category
            {
            };

            await mockedPostCategoryService.Run(mockedCategory);
        }
    }
}
