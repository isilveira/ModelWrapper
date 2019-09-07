using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWrapper.Binders
{
    public class WrapMemberBinder : SetMemberBinder
    {
        public WrapPropertySource Source { get; set; }

        public WrapMemberBinder(string name, WrapPropertySource source, bool ignoreCase) : base(name, ignoreCase)
        {
            Source = source;
        }

        public WrapMemberBinder(string name, bool ignoreCase) : base(name, ignoreCase)
        {
            Source = WrapPropertySource.FromBody;
        }

        public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
    public class WrapBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var model = Activator.CreateInstance(bindingContext.ModelType);
            
            if(bindingContext.BindingSource == BindingSource.Form)
            {
                bindingContext.Model = Activator.CreateInstance(bindingContext.ModelType);
                foreach(var key in bindingContext.HttpContext.Request.Form.Keys)
                {
                    var value = bindingContext.HttpContext.Request.Form[key];

                    var setMemberBinder = new WrapMemberBinder(key, WrapPropertySource.FromForm, true);

                    bindingContext.Model.GetType().GetMethod("TrySetMember").Invoke(bindingContext.Model, new object[] { setMemberBinder, value.ToString() });
                }
            }
            
            if (bindingContext.BindingSource == BindingSource.Body)
            {
                var body = string.Empty;
                using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    body = reader.ReadToEnd();
                }

                if (!string.IsNullOrWhiteSpace(body))
                {
                    var json = JObject.Parse(body);

                    JsonSerializer serializer = new JsonSerializer();
                    bindingContext.Model = serializer.Deserialize(json.CreateReader(), bindingContext.ModelType);
                }
            }

            var responsePropertiesValues = bindingContext.ValueProvider.GetValue("responseProperties").Values;

            if(bindingContext.Model == null)
            {
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
