using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for criteria
    /// </summary>
    internal static class CriteriasHelper
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
