using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Orders
{
    [TestClass]
    public class DeleteOrderServiceTest
    {
        private DeleteOrderService GetMockedDeleteOrderService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedOrderValidator = new OrderValidator();

            var mockedDeleteOrderSpecificationsValidator = new DeleteOrderSpecificationsValidator();

            var mockedDeleteOrderService = new DeleteOrderService(
                mockedDbContext.Object,
                mockedOrderValidator,
                mockedDeleteOrderSpecificationsValidator
                );

            return mockedDeleteOrderService;
        }

        [TestMethod]
        public async Task TestDeleteOrderValidModelAsync()
        {
            var mockedDeleteOrderService = GetMockedDeleteOrderService();

            var mockedOrder = new Order
            {
                Id = 1
            };

            await mockedDeleteOrderService.Run(mockedOrder);
        }
    }
}
