using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ModelWrapper.Helpers
{
    internal static class ReflectionHelper
    {
        internal static IList<PropertyInfo> GetPropertiesFromType(
            IList<PropertyInfo> searchableProperties,
            IList<string> queryProperties = null
        )
        {
            searchableProperties = searchableProperties.Where(x =>
                queryProperties == null
                || (queryProperties != null
                    && queryProperties.Count == 0)
                || queryProperties.Any(y => y.ToLower() == x.Name.ToLower())
            ).ToList();

            if (searchableProperties == null || searchableProperties.Count == 0)
            {
                throw new Exception("There's no searchable property found!");
            }

            return searchableProperties;
        }
        internal static MethodInfo GetMethodFromType(
            Type type,
            string methodName,
            int parameters,
            int genericArguments,
            List<Type> parameterTypes = null
        )
        {
            return type.GetMethods()
                .SingleOrDefault(method =>
                    method.Name == methodName
                    && method.GetParameters().Count() == parameters
                    && method.GetGenericArguments().Count() == genericArguments
                    && (parameterTypes == null
                        || parameterTypes.All(x => method.GetParameters().Select(parameter => parameter.ParameterType).Contains(x)))
                );
        }
        internal static Type CreateNewType(
            IList<PropertyInfo> props
        )
        {
            AssemblyName asmName = new AssemblyName("Wrapped");
            AssemblyBuilder dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("Wrapped");
            TypeBuilder dynamicAnonymousType = dynamicModule.DefineType("Wrapped", TypeAttributes.Public);

            foreach (var p in props)
            {
                var field = dynamicAnonymousType.DefineField("_" + p.Name, p.PropertyType, FieldAttributes.Private);
                var property = dynamicAnonymousType.DefineProperty(p.Name, PropertyAttributes.HasDefault, p.PropertyType, null);

                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                // Define the "get" accessor method for CustomerName.
                MethodBuilder propertyGet = dynamicAnonymousType.DefineMethod("get_" + p.Name, getSetAttr, p.PropertyType, Type.EmptyTypes);

                ILGenerator custNameGetIL = propertyGet.GetILGenerator();

                custNameGetIL.Emit(OpCodes.Ldarg_0);
                custNameGetIL.Emit(OpCodes.Ldfld, field);
                custNameGetIL.Emit(OpCodes.Ret);

                // Define the "set" accessor method for CustomerName.
                MethodBuilder propertySet = dynamicAnonymousType.DefineMethod("set_" + p.Name, getSetAttr, null, new Type[] { p.PropertyType });

                ILGenerator custNameSetIL = propertySet.GetILGenerator();

                custNameSetIL.Emit(OpCodes.Ldarg_0);
                custNameSetIL.Emit(OpCodes.Ldarg_1);
                custNameSetIL.Emit(OpCodes.Stfld, field);
                custNameSetIL.Emit(OpCodes.Ret);

                // Last, we must map the two methods created above to our PropertyBuilder to 
                // their corresponding behaviors, "get" and "set" respectively. 
                property.SetGetMethod(propertyGet);
                property.SetSetMethod(propertySet);
            }
            return dynamicAnonymousType.CreateType();
        }
    }
}
