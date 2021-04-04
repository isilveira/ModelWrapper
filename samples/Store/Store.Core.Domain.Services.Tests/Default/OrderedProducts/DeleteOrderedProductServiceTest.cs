using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.OrderedProducts
{
    [TestClass]
    public class DeleteOrderedProductServiceTest
    {
        private DeleteOrderedProductService GetMockedDeleteOrderedProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedOrderedProductValidator = new OrderedProductValidator();

            var mockedDeleteOrderedProductSpecificationsValidator = new DeleteOrderedProductSpecificationsValidator();

            var mockedDeleteOrderedProductService = new DeleteOrderedProductService(
                mockedDbContext.Object,
                mockedOrderedProductValidator,
                mockedDeleteOrderedProductSpecificationsValidator
                );

            return mockedDeleteOrderedProductService;
        }

        [TestMethod]
        public async Task TestDeleteOrderedProductValidModelAsync()
        {
            var mockedDeleteOrderedProductService = GetMockedDeleteOrderedProductService();

            var mockedOrderedProduct = new OrderedProduct
            {
                Id = 1
            };

            await mockedDeleteOrderedProductService.Run(mockedOrderedProduct);
        }
    }
}
