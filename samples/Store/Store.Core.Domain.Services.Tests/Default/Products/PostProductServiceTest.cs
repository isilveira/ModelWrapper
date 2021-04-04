using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Products
{
    [TestClass]
    public class PostProductServiceTest
    {
        private PostProductService GetMockedPostProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedProductValidator = new ProductValidator();

            var mockedPostProductSpecificationsValidator = new PostProductSpecificationsValidator();

            var mockedPostProductService = new PostProductService(
                mockedDbContext.Object,
                mockedProductValidator,
                mockedPostProductSpecificationsValidator);

            return mockedPostProductService;
        }

        [TestMethod]
        public async Task TestPostProductValidModelAsync()
        {
            var mockedPostProductService = GetMockedPostProductService();

            var mockedProduct = new Product
            {
            };

            await mockedPostProductService.Run(mockedProduct);
        }
    }
}
