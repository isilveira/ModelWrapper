using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for criteria
    /// </summary>
    internal static class CriteriaHelper
    {
        /// <summary>
        /// Method that gets possible criterias for some given type
        /// </summary>
        /// <param name="propertyType">type</param>
        /// <returns>List of valid criterias</returns>
        internal static List<string> GetPropertyTypeCriteria(
            Type propertyType
        )
        {
            List<string> comparationTypes = new List<string>();

            if (Nullable.GetUnderlyingType(propertyType) != null)
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
            }

            comparationTypes.Add("");
            comparationTypes.Add("_Not");
            if (propertyType == typeof(string) || propertyType == typeof(char))
            {
                comparationTypes.Add("_Contains");
                comparationTypes.Add("_NotContains");
                comparationTypes.Add("_StartsWith");
                comparationTypes.Add("_NotStartsWith");
                comparationTypes.Add("_EndsWith");
                comparationTypes.Add("_NotEndsWith");
            }

            if (propertyType == typeof(int)
                    || propertyType == typeof(long)
                    || propertyType == typeof(float)
                    || propertyType == typeof(float)
                    || propertyType == typeof(double)
                    || propertyType == typeof(decimal)
                    || propertyType == typeof(DateTime)
                    || propertyType == typeof(TimeSpan))
            {
                comparationTypes.Add("_GreaterThan");
                comparationTypes.Add("_GreaterThanOrEqual");
                comparationTypes.Add("_LessThan");
                comparationTypes.Add("_LessThanOrEqual");
            }

            return comparationTypes.Distinct().ToList();
        }
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

                object convertedObject = Convert.ChangeType(value, typeTo);
                changed = true;
                return convertedObject;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                changed = false;
                return Activator.CreateInstance<TReturn>();
            }
        }
        /// <summary>
        /// Method that returns a list of types that can't be converted
        /// </summary>
        /// <returns>List of types that can't be converted</returns>
        internal static IList<Type> GetNonQueryableTypes()
        {
            return new List<Type>
            {
                typeof(bool),
            };
        }
    }
}
