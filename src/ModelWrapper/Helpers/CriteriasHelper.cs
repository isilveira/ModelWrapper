using ModelWrapper.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for criteria
    /// </summary>
    internal static class CriteriasHelper
    {
        internal static string CriteriaWithSeparator(Criterias criteria) => $"_{criteria.GetDescription()}";
        public enum Criterias
        {
            [Description("")] Equal,
			[Description("Not")] NotEqual,
            [Description("In")] In,
            [Description("NotIn")] NotIn,
            [Description("Contains")] Contains,
            [Description("NotContains")] NotContains,
            [Description("StartsWith")] StartsWith,
            [Description("NotStartsWith")] NotStartsWith,
            [Description("EndsWith")] EndsWith,
            [Description("NotEndsWith")] NotEndsWith,
            [Description("GreaterThan")] GreaterThan,
            [Description("GreaterThanOrEqual")] GreaterThanOrEqual,
            [Description("LessThan")] LessThan,
			[Description("LessThanOrEqual")] LessThanOrEqual,
        }
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

            comparationTypes.Add(Criterias.Equal.GetDescription());
            comparationTypes.Add(CriteriaWithSeparator(Criterias.NotEqual));
			comparationTypes.Add(CriteriaWithSeparator(Criterias.In));
            comparationTypes.Add(CriteriaWithSeparator(Criterias.NotIn));
			if (propertyType == typeof(string) || propertyType == typeof(char))
            {
                comparationTypes.Add(CriteriaWithSeparator(Criterias.Contains));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.NotContains));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.StartsWith));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.NotStartsWith));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.EndsWith));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.NotEndsWith));
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
                comparationTypes.Add(CriteriaWithSeparator(Criterias.GreaterThan));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.GreaterThanOrEqual));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.LessThan));
                comparationTypes.Add(CriteriaWithSeparator(Criterias.LessThanOrEqual));
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
