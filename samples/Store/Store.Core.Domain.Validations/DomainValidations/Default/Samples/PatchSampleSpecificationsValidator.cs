using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Validations.Specifications.Default.Samples;
using NetDevPack.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Core.Domain.Validations.DomainValidations.Default.Samples
{
    public class PatchSampleSpecificationsValidator : SpecValidator<Sample>
    {
        public PatchSampleSpecificationsValidator(
            SampleDescriptionAlreadyExistsSpecification sampleDescriptionAlreadyExistsSpecification
        )
        {
            base.Add("SanpleMustBeUnique", new Rule<Sample>(sampleDescriptionAlreadyExistsSpecification.Not(), "A register with this description already exists!"));
        }
    }
}
