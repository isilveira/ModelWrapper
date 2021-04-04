using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.OrderedProducts;
using Store.Core.Domain.Validations.DomainValidations.Default.OrderedProducts;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.OrderedProducts
{
    [TestClass]
    public class PostOrderedProductServiceTest
    {
        private PostOrderedProductService GetMockedPostOrderedProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrderedProducts();

            var mockedOrderedProductValidator = new OrderedProductValidator();

            var mockedPostOrderedProductSpecificationsValidator = new PostOrderedProductSpecificationsValidator();

            var mockedPostOrderedProductService = new PostOrderedProductService(
                mockedDbContext.Object,
                mockedOrderedProductValidator,
                mockedPostOrderedProductSpecificationsValidator);

            return mockedPostOrderedProductService;
        }

        [TestMethod]
        public async Task TestPostOrderedProductValidModelAsync()
        {
            var mockedPostOrderedProductService = GetMockedPostOrderedProductService();

            var mockedOrderedProduct = new OrderedProduct
            {
            };

            await mockedPostOrderedProductService.Run(mockedOrderedProduct);
        }
    }
}
