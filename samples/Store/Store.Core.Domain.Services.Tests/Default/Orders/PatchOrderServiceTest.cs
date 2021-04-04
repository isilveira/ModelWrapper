using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Orders;
using Store.Core.Domain.Validations.DomainValidations.Default.Orders;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Orders
{
    [TestClass]
    public class PatchOrderServiceTest
    {
        private PatchOrderService GetMockedPatchOrderService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedOrders();

            var mockedOrderValidator = new OrderValidator();

            var mockedPatchOrderSpecificationsValidator = new PatchOrderSpecificationsValidator();

            var mockedPatchOrderService = new PatchOrderService(
                mockedDbContext.Object,
                mockedOrderValidator,
                mockedPatchOrderSpecificationsValidator
                );

            return mockedPatchOrderService;
        }

        [TestMethod]
        public async Task TestPatchOrderValidModelAsync()
        {
            var mockedPatchOrderService = GetMockedPatchOrderService();

            var mockedOrder = new Order
            {
                Id = 1,
            };

            await mockedPatchOrderService.Run(mockedOrder);
        }
    }
}
