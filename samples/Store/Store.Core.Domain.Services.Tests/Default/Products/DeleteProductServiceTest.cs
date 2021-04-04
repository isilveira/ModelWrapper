using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Products;
using Store.Core.Domain.Validations.DomainValidations.Default.Products;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Products
{
    [TestClass]
    public class DeleteProductServiceTest
    {
        private DeleteProductService GetMockedDeleteProductService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedProducts();

            var mockedProductValidator = new ProductValidator();

            var mockedDeleteProductSpecificationsValidator = new DeleteProductSpecificationsValidator();

            var mockedDeleteProductService = new DeleteProductService(
                mockedDbContext.Object,
                mockedProductValidator,
                mockedDeleteProductSpecificationsValidator
                );

            return mockedDeleteProductService;
        }

        [TestMethod]
        public async Task TestDeleteProductValidModelAsync()
        {
            var mockedDeleteProductService = GetMockedDeleteProductService();

            var mockedProduct = new Product
            {
                Id = 1
            };

            await mockedDeleteProductService.Run(mockedProduct);
        }
    }
}
