using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ModelWrapper.Binders
{
    public class WrapRequestBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var model = Activator.CreateInstance(bindingContext.ModelType);

            if (bindingContext.HttpContext.Request.HasFormContentType)
            {
                bindingContext.HttpContext.Request.Form.Keys.ToList().ForEach(key =>
                {
                    model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] {
                        new WrapRequestMemberBinder(key, WrapPropertySource.FromForm, true),
                        bindingContext.HttpContext.Request.Form[key].ToString()
                    });
                });
            }

            if (bindingContext.HttpContext.Request.ContentLength != null)
            {
                var body = string.Empty;
                using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
                {
                    body = await reader.ReadToEndAsync();
                }

                if (!string.IsNullOrWhiteSpace(body))
                {
                    var json = JObject.Parse(body);

                    JsonSerializer serializer = new JsonSerializer();
                    model = serializer.Deserialize(json.CreateReader(), bindingContext.ModelType);
                }
            }

            if (bindingContext.HttpContext.Request.Query.Count > 0)
            {
                bindingContext.HttpContext.Request.Query.Keys.ToList().ForEach(queryProperty =>
                {
                    var memberBinder = new WrapRequestMemberBinder(queryProperty, WrapPropertySource.FromQuery, true);
                    var values = bindingContext.HttpContext.Request.Query[queryProperty].ToList();

                    values.ToList().ForEach(value =>
                    {
                        model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] {
                            memberBinder,
                            value.ToString()
                        });
                    });
                });
            }

            if (bindingContext.ActionContext.RouteData.Values.Any())
            {
                bindingContext.ActionContext.RouteData.Values.ToList().ForEach(routeProperty =>
                {
                    model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] {
                        new WrapRequestMemberBinder(routeProperty.Key, WrapPropertySource.FromRoute, true),
                        routeProperty.Value.ToString()
                    });
                });
            }

            model.GetType().GetMethod("BindComplete").Invoke(model, new object[] { });

            bindingContext.Model = model;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        }
    }
}
