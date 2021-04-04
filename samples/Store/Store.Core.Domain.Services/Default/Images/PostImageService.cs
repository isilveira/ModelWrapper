using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Images;
using Store.Core.Domain.Validations.DomainValidations.Default.Images;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Images
{
    public class PostImageService : DomainService<Image>, IPostImageService
    {
        private IDefaultDbContext Context { get; set; }
        public PostImageService(
            IDefaultDbContext context,
            ImageValidator entityValidator,
            PostImageSpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(Image entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.Images.AddAsync(entity);
        }
    }
}
