using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Images
{
    [TestClass]
    public class DeleteImageServiceTest
    {
        private DeleteImageService GetMockedDeleteImageService()
        {
            var mockedDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedImages();

            var mockedImageValidator = new ImageValidator();

            var mockedDeleteImageSpecificationsValidator = new DeleteImageSpecificationsValidator();

            var mockedDeleteImageService = new DeleteImageService(
                mockedDbContext.Object,
                mockedImageValidator,
                mockedDeleteImageSpecificationsValidator
                );

            return mockedDeleteImageService;
        }

        [TestMethod]
        public async Task TestDeleteImageValidModelAsync()
        {
            var mockedDeleteImageService = GetMockedDeleteImageService();

            var mockedImage = new Image
            {
                Id = 1
            };

            await mockedDeleteImageService.Run(mockedImage);
        }
    }
}
