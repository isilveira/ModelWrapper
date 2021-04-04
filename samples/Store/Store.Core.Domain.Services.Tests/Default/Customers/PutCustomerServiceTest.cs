using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Customers
{

    [TestClass]
    public class PutCustomerServiceTest
    {
        private PutCustomerService GetMockedPutCustomerService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedCustomerValidator = new CustomerValidator();

            var mockedPutCustomerSpecificationsValidator = new PutCustomerSpecificationsValidator();

            var mockedPutCustomerService = new PutCustomerService(
                mockedDbContext.Object,
                mockedCustomerValidator,
                mockedPutCustomerSpecificationsValidator);

            return mockedPutCustomerService;
        }

        [TestMethod]
        public async Task TestPutCustomerValidModelAsync()
        {
            var mockedPutCustomerService = GetMockedPutCustomerService();

            var mockedCustomer = new Customer
            {
                Id = 1,
            };

            await mockedPutCustomerService.Run(mockedCustomer);
        }
    }
}
