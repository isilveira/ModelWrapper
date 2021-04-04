using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Categories
{
    [TestClass]
    public class DeleteCategoryServiceTest
    {
        private DeleteCategoryService GetMockedDeleteCategoryService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCategories();

            var mockedCategoryValidator = new CategoryValidator();

            var mockedDeleteCategorySpecificationsValidator = new DeleteCategorySpecificationsValidator();

            var mockedDeleteCategoryService = new DeleteCategoryService(
                mockedDbContext.Object,
                mockedCategoryValidator,
                mockedDeleteCategorySpecificationsValidator
                );

            return mockedDeleteCategoryService;
        }

        [TestMethod]
        public async Task TestDeleteCategoryValidModelAsync()
        {
            var mockedDeleteCategoryService = GetMockedDeleteCategoryService();

            var mockedCategory = new Category
            {
                Id = 1
            };

            await mockedDeleteCategoryService.Run(mockedCategory);
        }
    }
}
