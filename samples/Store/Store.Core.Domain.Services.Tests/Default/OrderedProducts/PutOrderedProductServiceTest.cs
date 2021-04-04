using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.OrderedProducts
{

    [TestClass]
    public class PutOrderedProductServiceTest
    {
        private PutOrderedProductService GetMockedPutOrderedProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedOrderedProductValidator = new OrderedProductValidator();

            var mockedPutOrderedProductSpecificationsValidator = new PutOrderedProductSpecificationsValidator();

            var mockedPutOrderedProductService = new PutOrderedProductService(
                mockedDbContext.Object,
                mockedOrderedProductValidator,
                mockedPutOrderedProductSpecificationsValidator);

            return mockedPutOrderedProductService;
        }

        [TestMethod]
        public async Task TestPutOrderedProductValidModelAsync()
        {
            var mockedPutOrderedProductService = GetMockedPutOrderedProductService();

            var mockedOrderedProduct = new OrderedProduct
            {
                Id = 1,
            };

            await mockedPutOrderedProductService.Run(mockedOrderedProduct);
        }
    }
}
