using Store.Core.Domain.Entities.Default;

namespace Store.Core.Application.Default.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommand : ApplicationRequest<Category, PatchCategoryCommandResponse>
    {
        public PatchCategoryCommand()
        {
            ConfigKeys(x => x.Id);

            // Configures supressed properties & response properties
            ConfigSuppressedProperties(x => x.RootCategory);
            ConfigSuppressedProperties(x => x.SubCategories);
            ConfigSuppressedProperties(x => x.Products);

            ConfigSuppressedResponseProperties(x => x.RootCategory);
            ConfigSuppressedResponseProperties(x => x.SubCategories);
            ConfigSuppressedResponseProperties(x => x.Products);
        }
    }
}
