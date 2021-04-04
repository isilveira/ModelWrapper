using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Images
{
    [TestClass]
    public class PostImageServiceTest
    {
        private PostImageService GetMockedPostImageService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedDbContextQuery = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedImageValidator = new ImageValidator();

            var mockedPostImageSpecificationsValidator = new PostImageSpecificationsValidator();

            var mockedPostImageService = new PostImageService(
                mockedDbContext.Object,
                mockedImageValidator,
                mockedPostImageSpecificationsValidator);

            return mockedPostImageService;
        }

        [TestMethod]
        public async Task TestPostImageValidModelAsync()
        {
            var mockedPostImageService = GetMockedPostImageService();

            var mockedImage = new Image
            {
            };

            await mockedPostImageService.Run(mockedImage);
        }
    }
}
