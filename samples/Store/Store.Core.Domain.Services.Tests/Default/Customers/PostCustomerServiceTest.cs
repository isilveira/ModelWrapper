using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Customers
{
    [TestClass]
    public class PostCustomerServiceTest
    {
        private PostCustomerService GetMockedPostCustomerService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedCustomerValidator = new CustomerValidator();

            var mockedPostCustomerSpecificationsValidator = new PostCustomerSpecificationsValidator();

            var mockedPostCustomerService = new PostCustomerService(
                mockedDbContext.Object,
                mockedCustomerValidator,
                mockedPostCustomerSpecificationsValidator);

            return mockedPostCustomerService;
        }

        [TestMethod]
        public async Task TestPostCustomerValidModelAsync()
        {
            var mockedPostCustomerService = GetMockedPostCustomerService();

            var mockedCustomer = new Customer
            {
            };

            await mockedPostCustomerService.Run(mockedCustomer);
        }
    }
}
