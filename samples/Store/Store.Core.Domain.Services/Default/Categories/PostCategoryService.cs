using Store.Core.Domain.Entities.Default;
using Store.Core.Domain.Interfaces.Infrastructures.Data.Contexts;
using Store.Core.Domain.Interfaces.Services.Default.Categories;
using Store.Core.Domain.Validations.DomainValidations.Default.Categories;
using Store.Core.Domain.Validations.EntityValidations.Default;
using System.Threading.Tasks;

namespace Store.Core.Domain.Services.Default.Categories
{
    public class PostCategoryService : DomainService<Category>, IPostCategoryService
    {
        private IDefaultDbContext Context { get; set; }
        public PostCategoryService(
            IDefaultDbContext context,
            CategoryValidator entityValidator,
            PostCategorySpecificationsValidator domainValidator
        ) : base(entityValidator, domainValidator)
        {
            Context = context;
        }
        public override async Task Run(Category entity)
        {
            ValidateEntity(entity);

            ValidateDomain(entity);

            await Context.Categories.AddAsync(entity);
        }
    }
}
