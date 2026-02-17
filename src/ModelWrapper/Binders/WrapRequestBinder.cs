using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelWrapper.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ModelWrapper.Binders
{
    /// <summary>
    /// Class that binds the http request into a WrapRequest object
    /// </summary>
    public class WrapRequestBinder : IModelBinder
    {
        /// <summary>
        /// Async method that proceed bind of wraprequest
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public async Task BindModelAsync(
            ModelBindingContext bindingContext
        )
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var model = Activator.CreateInstance(bindingContext.ModelType);

            if (bindingContext.HttpContext.Request.HasFormContentType)
            {
                bindingContext.HttpContext.Request.Form.Keys.ToList().ForEach(key =>
                {
                    model.GetType().GetMethod("TrySetMember").Invoke(model, new object[] {
                        new WrapRequestMemberBinder(key, WrapPropertySource.FromForm, true),
                        bindingContext.HttpContext.Request.Form[key].ToString().Equals("null") ? null : bindingContext.HttpContext.Request.Form[key].ToString()
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
                    var trimmedBody = body.TrimStart();
                    var isBatchArray = trimmedBody.StartsWith("[");
                    var modelType = bindingContext.ModelType;

                    var batchModelType = GetBatchModelType(modelType);
                    if (isBatchArray && batchModelType != null)
                    {
                        var elementType = TypesHelper.GetEntityTypeFromComplex(batchModelType);
                        var listType = typeof(List<>).MakeGenericType(elementType);
                        var serializer = new JsonSerializer();
                        IList batchData;

                        using (var jsonReader = new JTokenReader(JArray.Parse(body)))
                        {
                            batchData = (IList)serializer.Deserialize(jsonReader, listType);
                        }

                        if (batchData != null)
                        {
                            var currentType = modelType;
                            while (currentType != null)
                            {
                                var modelProperty = currentType.GetProperty("Model", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                                if (modelProperty != null)
                                {
                                    modelProperty.SetValue(model, batchData);
                                    break;
                                }
                                currentType = currentType.BaseType;
                            }
                        }
                    }
                    else if (trimmedBody.StartsWith("{"))
                    {
                        var json = JObject.Parse(body);
                        var serializer = new JsonSerializer();
                        model = serializer.Deserialize(json.CreateReader(), bindingContext.ModelType);
                    }
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

        /// <summary>
        /// Traverses the type hierarchy to find the batch model type (e.g. List&lt;T&gt;) when the parameter is a concrete class like BatchPostEventCommand.
        /// </summary>
        private static Type GetBatchModelType(Type modelType)
        {
            var currentType = modelType;
            while (currentType != null)
            {
                if (currentType.IsGenericType && currentType.GenericTypeArguments.Length > 0)
                {
                    var firstArg = currentType.GenericTypeArguments[0];
                    if (TypesHelper.IsBatchModel(firstArg))
                        return firstArg;
                }
                currentType = currentType.BaseType;
            }
            return null;
        }
    }
}
