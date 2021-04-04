using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Products
{

    [TestClass]
    public class PutProductServiceTest
    {
        private PutProductService GetMockedPutProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedProductValidator = new ProductValidator();

            var mockedPutProductSpecificationsValidator = new PutProductSpecificationsValidator();

            var mockedPutProductService = new PutProductService(
                mockedDbContext.Object,
                mockedProductValidator,
                mockedPutProductSpecificationsValidator);

            return mockedPutProductService;
        }

        [TestMethod]
        public async Task TestPutProductValidModelAsync()
        {
            var mockedPutProductService = GetMockedPutProductService();

            var mockedProduct = new Product
            {
                Id = 1,
            };

            await mockedPutProductService.Run(mockedProduct);
        }
    }
}
