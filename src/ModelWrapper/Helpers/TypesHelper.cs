using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ModelWrapper.Helpers
{
    internal static class TypesHelper
    {
        /// <summary>
        /// Method that try to convert string into a type object
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="typeTo">Type to convert</param>
        /// <param name="changed">Boolean that indicate if change has succeed</param>
        /// <returns>object conveted</returns>
        internal static object TryChangeType(
            string value,
            Type typeTo,
            out bool changed
        )
        {
            try
            {
                if (Nullable.GetUnderlyingType(typeTo) != null)
                {
                    if (!string.IsNullOrWhiteSpace(value) && value == "null")
                    {
                        changed = true;
                        return Activator.CreateInstance(typeTo);
                    }
                    else
                    {
                        typeTo = Nullable.GetUnderlyingType(typeTo);
                    }
                }

                object convertedObject = typeTo == typeof(Guid) ? Guid.Parse(value) : Convert.ChangeType(value, typeTo);
                changed = true;
                return convertedObject;
            }
            catch (Exception)
            {
                changed = false;
                return Activator.CreateInstance(typeTo);
            }
        }
        /// <summary>
        /// Method that try to convert string into a type object
        /// </summary>
        /// <typeparam name="TReturn">Type to convert</typeparam>
        /// <param name="value">String value</param>
        /// <param name="changed">Boolean that indicate if change has succeed</param>
        /// <returns>object conveted</returns>
        internal static TReturn TryChangeType<TReturn>(
            string value,
            out bool changed
        )
        {
            Type typeTo = typeof(TReturn);
            try
            {
                if (Nullable.GetUnderlyingType(typeTo) != null)
                {
                    if (!string.IsNullOrWhiteSpace(value) && value == "null")
                    {
                        changed = true;
                        return Activator.CreateInstance<TReturn>();
                    }
                    else
                    {
                        typeTo = Nullable.GetUnderlyingType(typeTo);
                    }
                }

                object convertedObject = Convert.ChangeType(value, typeTo);
                changed = true;
                return (TReturn)convertedObject;
            }
            catch (Exception)
            {
                changed = false;
                return Activator.CreateInstance<TReturn>();
            }
        }
        internal static bool TypeIsClass(Type type)
        {
            return type.IsClass;
        }
        internal static bool TypeIsCollection(Type type)
        {
            var collectionTypes = new List<Type> {
                typeof(ICollection<>),
                typeof(IEnumerable<>),
                typeof(IList<>),
                typeof(Collection<>),
                //typeof(Enumerable<>),
                typeof(List<>),
                typeof(HashSet<>),
            };

            return type.IsGenericType && (type.IsArray || collectionTypes.Any(collectionType => collectionType == type.GetGenericTypeDefinition()));
        }
        internal static bool TypeIsEntity(Type type)
        {
            return ConfigurationService.GetConfiguration().EntityBase != null && ConfigurationService.GetConfiguration().EntityBase.IsAssignableFrom(type);
        }
        internal static bool TypeIsComplex(Type type)
        {
            return (TypeIsClass(type) && TypeIsEntity(type)) || (TypeIsCollection(type) && TypeIsEntity(type.GenericTypeArguments[0]));
        }
        internal static Type GetEntityTypeFromComplex(Type type)
        {
            return TypeIsCollection(type) ? type.GenericTypeArguments[0] : type;
        }
    }
}
