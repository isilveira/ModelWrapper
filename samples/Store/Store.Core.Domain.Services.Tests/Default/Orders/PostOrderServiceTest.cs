using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Orders
{
    [TestClass]
    public class PostOrderServiceTest
    {
        private PostOrderService GetMockedPostOrderService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedOrderValidator = new OrderValidator();

            var mockedPostOrderSpecificationsValidator = new PostOrderSpecificationsValidator();

            var mockedPostOrderService = new PostOrderService(
                mockedDbContext.Object,
                mockedOrderValidator,
                mockedPostOrderSpecificationsValidator);

            return mockedPostOrderService;
        }

        [TestMethod]
        public async Task TestPostOrderValidModelAsync()
        {
            var mockedPostOrderService = GetMockedPostOrderService();

            var mockedOrder = new Order
            {
            };

            await mockedPostOrderService.Run(mockedOrder);
        }
    }
}
