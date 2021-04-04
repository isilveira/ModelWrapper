using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Customers;
using Store.Core.Domain.Validations.DomainValidations.Default.Customers;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Customers
{
    [TestClass]
    public class DeleteCustomerServiceTest
    {
        private DeleteCustomerService GetMockedDeleteCustomerService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedCustomers();

            var mockedCustomerValidator = new CustomerValidator();

            var mockedDeleteCustomerSpecificationsValidator = new DeleteCustomerSpecificationsValidator();

            var mockedDeleteCustomerService = new DeleteCustomerService(
                mockedDbContext.Object,
                mockedCustomerValidator,
                mockedDeleteCustomerSpecificationsValidator
                );

            return mockedDeleteCustomerService;
        }

        [TestMethod]
        public async Task TestDeleteCustomerValidModelAsync()
        {
            var mockedDeleteCustomerService = GetMockedDeleteCustomerService();

            var mockedCustomer = new Customer
            {
                Id = 1
            };

            await mockedDeleteCustomerService.Run(mockedCustomer);
        }
    }
}
