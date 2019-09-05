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

                    ///SetMemberBinder setMemberBinder = new SetMemberBinder(key);

                    //var method = bindingContext.Model.GetType().GetMethod("TrySetMember").Invoke(bindingContext.Model, new object[] { setMemberBinder, value });

                    var property = bindingContext.ModelType.BaseType.GetGenericArguments()[0].GetProperties().SingleOrDefault(x => x.Name.ToLower().Equals(key.ToLower()));
                    property.SetValue(bindingContext.Model, value);

                }
            }

            if (bindingContext.BindingSource == BindingSource.Body)
            {
                var body = string.Empty;
                using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    body = reader.ReadToEnd();
                }

                var json = JObject.Parse(body);

                JsonSerializer serializer = new JsonSerializer();
                bindingContext.Model = serializer.Deserialize(json.CreateReader(), bindingContext.ModelType);
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
