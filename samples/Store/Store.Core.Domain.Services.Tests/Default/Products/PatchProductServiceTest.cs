using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Products
{
    [TestClass]
    public class PatchProductServiceTest
    {
        private PatchProductService GetMockedPatchProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedProductValidator = new ProductValidator();

            var mockedPatchProductSpecificationsValidator = new PatchProductSpecificationsValidator();

            var mockedPatchProductService = new PatchProductService(
                mockedDbContext.Object,
                mockedProductValidator,
                mockedPatchProductSpecificationsValidator
                );

            return mockedPatchProductService;
        }

        [TestMethod]
        public async Task TestPatchProductValidModelAsync()
        {
            var mockedPatchProductService = GetMockedPatchProductService();

            var mockedProduct = new Product
            {
                Id = 1,
            };

            await mockedPatchProductService.Run(mockedProduct);
        }
    }
}
