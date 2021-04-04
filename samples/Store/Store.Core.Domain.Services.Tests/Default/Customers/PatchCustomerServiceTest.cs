using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Customers
{
    [TestClass]
    public class PatchCustomerServiceTest
    {
        private PatchCustomerService GetMockedPatchCustomerService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedCustomerValidator = new CustomerValidator();

            var mockedPatchCustomerSpecificationsValidator = new PatchCustomerSpecificationsValidator();

            var mockedPatchCustomerService = new PatchCustomerService(
                mockedDbContext.Object,
                mockedCustomerValidator,
                mockedPatchCustomerSpecificationsValidator
                );

            return mockedPatchCustomerService;
        }

        [TestMethod]
        public async Task TestPatchCustomerValidModelAsync()
        {
            var mockedPatchCustomerService = GetMockedPatchCustomerService();

            var mockedCustomer = new Customer
            {
                Id = 1,
            };

            await mockedPatchCustomerService.Run(mockedCustomer);
        }
    }
}
