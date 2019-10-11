using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Extensions
{
    internal static class GenericExtensions
    {
        internal static bool IsDefault<T>(this T source)
        {
            return source.Equals(default(T));
        }
        internal static TValue GetValue<TKey,TValue>(this Dictionary<TKey, TValue> source, TKey key)
        {
            TValue value = default(TValue);

            var success = source.TryGetValue(key, out value);

            return success ? value : Activator.CreateInstance<TValue>();
        }
    }
}
