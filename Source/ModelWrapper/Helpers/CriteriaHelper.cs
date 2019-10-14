using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelWrapper.Helpers
{
    internal static class CriteriaHelper
    {
        internal static List<string> GetPropertyTypeCriteria(Type propertyType)
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

        internal static object TryChangeType(string value, Type typeTo, ref bool changed)
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

        internal static TReturn TryChangeType<TReturn>(string value, ref bool changed)
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
        internal static IList<Type> GetNonQueryableTypes()
        {
            return new List<Type>
            {
                typeof(bool),
            };
        }
    }
}
