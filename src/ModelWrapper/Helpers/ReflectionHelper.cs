using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for reflection handling
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// Method that filter properties from list
        /// </summary>
        /// <param name="searchableProperties">List of properties</param>
        /// <param name="queryProperties">Properties names to find</param>
        /// <returns>List of properties found</returns>
        internal static IList<PropertyInfo> GetFilterPropertiesFromList(
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
        /// <summary>
        /// Method that find a method from a given type
        /// </summary>
        /// <param name="type">Type were method must be found</param>
        /// <param name="methodName">Name of the method</param>
        /// <param name="parameters">Number of parameters</param>
        /// <param name="genericArguments">Number of arguments</param>
        /// <param name="parameterTypes">List of parameter types</param>
        /// <returns>Method found</returns>
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
        /// <summary>
        /// Method that creates a runtime type
        /// </summary>
        /// <param name="props">Properties for the new type</param>
        /// <param name="assemblyName">Assembly name</param>
        /// <param name="moduleName">Module name</param>
        /// <param name="typeName">Type name</param>
        /// <returns>Runtime new type</returns>
        internal static Type CreateNewType(
            IList<PropertyInfo> props,
            string assemblyName = "ModelWrapper",
            string moduleName = "DynamicTypes",
            string typeName = "SelectWrap"
        )
        {
            AssemblyName asmName = new AssemblyName(assemblyName);
            AssemblyBuilder dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule(moduleName);
            TypeBuilder dynamicAnonymousType = dynamicModule.DefineType(typeName, TypeAttributes.Public);

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
            return dynamicAnonymousType.CreateTypeInfo().AsType();
        }

        internal static TCopyTo Copy<TCopyFrom, TCopyTo>(TCopyFrom from, TCopyTo to)
        {
            foreach(var property in from.GetType().GetProperties())
            {
                var toProperty = to.GetType().GetProperty(property.Name);

                if (toProperty != null)
                {
                    toProperty.SetValue(to, property.GetValue(from)); 
                }
            }

            return to;
        }
    }
}