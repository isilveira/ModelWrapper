using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Samples;
using Store.Core.Domain.Validations.DomainValidations.Default.Samples;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Samples
{
    [TestClass]
    public class DeleteSampleServiceTest
    {
        private DeleteSampleService GetMockedDeleteSampleService()
        {
            var mockedDefaultDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedSamples();

            var mockedSampleValidator = new SampleValidator();

            var mockedDeleteSampleSpecificationsValidator = new DeleteSampleSpecificationsValidator();

            var mockedDeleteSampleService = new DeleteSampleService(
                mockedDefaultDbContext.Object,
                mockedSampleValidator,
                mockedDeleteSampleSpecificationsValidator
                );

            return mockedDeleteSampleService;
        }

        [TestMethod]
        public async Task TestDeleteSampleValidModelAsync()
        {
            var mockedDeleteSampleService = GetMockedDeleteSampleService();

            var mockedSample = new Sample
            {
                Id = 1,
                Description = "Sample - 001"
            };

            await mockedDeleteSampleService.Run(mockedSample);
        }
    }
}
