﻿using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Exceptions;
using Store.Core.Domain.Services.Default.Samples;
using Store.Core.Domain.Validations.DomainValidations.Default.Samples;
using Store.Core.Domain.Validations.EntityValidations.Default;
using Store.Core.Domain.Validations.Specifications.Default.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Tests.Default.Samples
{
    [TestClass]
    public class PatchSampleServiceTest
    {
        private PatchSampleService GetMockedPatchSampleService()
        {
            var mockedDefaultDbContext = MockDefaultHelper
                .GetMockedDbContext()
                .AddMockedSamples();

            var mockedDefaultDbContextQuery = MockDefaultHelper
                .GetMockedDbContextQuery()
                .AddMockedSamples();

            var mockedSampleValidator = new SampleValidator();

            var sampleDescriptionAlreadyExistsSpecification = new SampleDescriptionAlreadyExistsSpecification(
                mockedDefaultDbContextQuery.Object);

            var mockedPatchSampleSpecificationsValidator = new PatchSampleSpecificationsValidator(
                sampleDescriptionAlreadyExistsSpecification);

            var mockedPatchSampleService = new PatchSampleService(
                mockedDefaultDbContext.Object,
                mockedSampleValidator,
                mockedPatchSampleSpecificationsValidator
                );

            return mockedPatchSampleService;
        }

        [TestMethod]
        public async Task TestPatchSampleWithEmptyModelAsync()
        {
            var mockedPatchSampleService = GetMockedPatchSampleService();

            var mockedSample = new Sample { };

            await Assert.ThrowsExceptionAsync<BusinessException>(() =>
                mockedPatchSampleService.Run(mockedSample));
        }

        [TestMethod]
        public async Task TestPatchSampleWithDuplicatedDescriptionOnSchoolAsync()
        {
            var mockedPatchSampleService = GetMockedPatchSampleService();

            var mockedSample = new Sample
            {
                Id = 1,
                Description = "Sample - 002"
            };

            await Assert.ThrowsExceptionAsync<BusinessException>(() =>
                mockedPatchSampleService.Run(mockedSample));
        }

        [TestMethod]
        public async Task TestPatchSampleValidModelAsync()
        {
            var mockedPatchSampleService = GetMockedPatchSampleService();

            var mockedSample = new Sample
            {
                Id = 1,
                Description = "Sample - 003"
            };

            await mockedPatchSampleService.Run(mockedSample);
        }
    }
}
