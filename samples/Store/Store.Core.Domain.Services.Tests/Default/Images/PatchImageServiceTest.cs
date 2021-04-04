using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Images
{
    [TestClass]
    public class PatchImageServiceTest
    {
        private PatchImageService GetMockedPatchImageService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedImageValidator = new ImageValidator();

            var mockedPatchImageSpecificationsValidator = new PatchImageSpecificationsValidator();

            var mockedPatchImageService = new PatchImageService(
                mockedDbContext.Object,
                mockedImageValidator,
                mockedPatchImageSpecificationsValidator
                );

            return mockedPatchImageService;
        }

        [TestMethod]
        public async Task TestPatchImageValidModelAsync()
        {
            var mockedPatchImageService = GetMockedPatchImageService();

            var mockedImage = new Image
            {
                Id = 1,
            };

            await mockedPatchImageService.Run(mockedImage);
        }
    }
}
