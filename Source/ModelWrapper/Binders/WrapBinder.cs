using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModelWrapper.Binders
{
    public class WrapBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var model = Activator.CreateInstance(bindingContext.ModelType);

            var responsePropertiesValues = bindingContext.ValueProvider.GetValue("responseProperties").Values;

            return Task.CompletedTask;
        }
    }
}
