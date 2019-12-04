using System;
using System.Collections.Generic;
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
    }
}
