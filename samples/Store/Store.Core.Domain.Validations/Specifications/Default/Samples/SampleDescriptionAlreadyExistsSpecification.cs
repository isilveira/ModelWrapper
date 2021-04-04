using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using NetDevPack.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Store.Core.Domain.Validations.Specifications.Default.Samples
{
    public class SampleDescriptionAlreadyExistsSpecification : Specification<Sample>
    {
        private IDefaultDbContextQuery Context { get; set; }
        public SampleDescriptionAlreadyExistsSpecification(IDefaultDbContextQuery context)
        {
            Context = context;
        }

        public override Expression<Func<Sample, bool>> ToExpression()
        {
            return sample => Context.Samples.Any(x => x.Description == sample.Description && x.Id != sample.Id);
        }
    }
}
