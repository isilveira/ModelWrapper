using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.OrderedProducts
{
    [TestClass]
    public class PatchOrderedProductServiceTest
    {
        private PatchOrderedProductService GetMockedPatchOrderedProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedOrderedProductValidator = new OrderedProductValidator();

            var mockedPatchOrderedProductSpecificationsValidator = new PatchOrderedProductSpecificationsValidator();

            var mockedPatchOrderedProductService = new PatchOrderedProductService(
                mockedDbContext.Object,
                mockedOrderedProductValidator,
                mockedPatchOrderedProductSpecificationsValidator
                );

            return mockedPatchOrderedProductService;
        }

        [TestMethod]
        public async Task TestPatchOrderedProductValidModelAsync()
        {
            var mockedPatchOrderedProductService = GetMockedPatchOrderedProductService();

            var mockedOrderedProduct = new OrderedProduct
            {
                Id = 1,
            };

            await mockedPatchOrderedProductService.Run(mockedOrderedProduct);
        }
    }
}
