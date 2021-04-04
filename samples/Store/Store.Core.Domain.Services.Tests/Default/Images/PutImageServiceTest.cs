using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Images
{

    [TestClass]
    public class PutImageServiceTest
    {
        private PutImageService GetMockedPutImageService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedImageValidator = new ImageValidator();

            var mockedPutImageSpecificationsValidator = new PutImageSpecificationsValidator();

            var mockedPutImageService = new PutImageService(
                mockedDbContext.Object,
                mockedImageValidator,
                mockedPutImageSpecificationsValidator);

            return mockedPutImageService;
        }

        [TestMethod]
        public async Task TestPutImageValidModelAsync()
        {
            var mockedPutImageService = GetMockedPutImageService();

            var mockedImage = new Image
            {
                Id = 1,
            };

            await mockedPutImageService.Run(mockedImage);
        }
    }
}
