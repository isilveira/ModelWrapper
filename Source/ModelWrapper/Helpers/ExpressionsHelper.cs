using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ModelWrapper.Helpers
{
    public static class ExpressionsHelper
    {
        public static Expression<Func<TSource, object>> SelectExpression<TSource>(IList<PropertyInfo> properties)
        {
            var entityType = typeof(TSource);
            var source = Expression.Parameter(entityType, "x");

            var newType = CreateNewType(properties);

            var binding = properties.ToList().Select(p => Expression.Bind(newType.GetProperty(p.Name), Expression.Property(source, p.Name))).ToList();
            var body = Expression.MemberInit(Expression.New(newType), binding);
            var selector = Expression.Lambda<Func<TSource, object>>(body, source);

            return selector;
        }
        public static Type CreateNewType(IList<PropertyInfo> props)
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
