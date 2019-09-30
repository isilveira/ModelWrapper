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

            if (bindingContext.HttpContext.Request.HasFormContentType)
            {
                foreach (var key in bindingContext.HttpContext.Request.Form.Keys)
                {
                    var value = bindingContext.HttpContext.Request.Form[key];

                    var setMemberBinder = new WrapMemberBinder(key, WrapPropertySource.FromForm, true);

                    model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] { setMemberBinder, value.ToString() });
                }
            }

            if (bindingContext.HttpContext.Request.ContentLength != null)
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
                    model = serializer.Deserialize(json.CreateReader(), bindingContext.ModelType);
                }
            }

            if (bindingContext.HttpContext.Request.Query.Count > 0)
            {
                foreach (var queryProperty in bindingContext.HttpContext.Request.Query.Keys)
                {
                    var memberBinder = new WrapMemberBinder(queryProperty, WrapPropertySource.FromQuery, true);
                    var value = bindingContext.HttpContext.Request.Query[queryProperty];

                    if(value.Count > 1)
                    {
                        foreach(var v in value)
                        {
                            model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] { memberBinder, v.ToString() });
                        }
                    }
                    else
                    {
                        model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] { memberBinder, value.ToString() });
                    }
                }
            }

            if (bindingContext.ActionContext.RouteData.Values.Count > 0)
            {
                foreach (var routeProperty in bindingContext.ActionContext.RouteData.Values.ToList())
                {
                    var memberBinder = new WrapMemberBinder(routeProperty.Key, WrapPropertySource.FromRoute, true);
                    var value = routeProperty.Value;
                    model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] { memberBinder, value.ToString() });
                }
            }

            model.GetType().GetMethod("ProcessBind").Invoke(model, new object[] { });

            bindingContext.Model = model;

            if (bindingContext.Model == null)
            {
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
