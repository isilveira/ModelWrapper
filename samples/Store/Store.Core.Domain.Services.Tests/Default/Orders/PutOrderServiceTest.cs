using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Orders
{

    [TestClass]
    public class PutOrderServiceTest
    {
        private PutOrderService GetMockedPutOrderService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedOrderValidator = new OrderValidator();

            var mockedPutOrderSpecificationsValidator = new PutOrderSpecificationsValidator();

            var mockedPutOrderService = new PutOrderService(
                mockedDbContext.Object,
                mockedOrderValidator,
                mockedPutOrderSpecificationsValidator);

            return mockedPutOrderService;
        }

        [TestMethod]
        public async Task TestPutOrderValidModelAsync()
        {
            var mockedPutOrderService = GetMockedPutOrderService();

            var mockedOrder = new Order
            {
                Id = 1,
            };

            await mockedPutOrderService.Run(mockedOrder);
        }
    }
}
