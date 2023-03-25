﻿using BAYSOFT.Abstractions.Core.Domain.Validations;
using BAYSOFT.Core.Domain.Default.Entities;
using BAYSOFT.Core.Domain.Default.Specifications.Samples;

namespace BAYSOFT.Core.Domain.Default.Validations.DomainValidations.Samples
{
    public class CreateSampleSpecificationsValidator : DomainValidator<Sample>
    {
        public CreateSampleSpecificationsValidator(
            SampleDescriptionAlreadyExistsSpecification sampleDescriptionAlreadyExistsSpecification
        )
        {
            base.Add("SanpleMustBeUnique", new DomainRule<Sample>(sampleDescriptionAlreadyExistsSpecification.Not(), "A register with this description already exists!"));
        }
    }
}
