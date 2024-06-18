using EnumsNET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ModelWrapper.Extensions
{
	/// <summary>
	/// Class that extends generic types
	/// </summary>
	internal static class GenericExtensions
	{
		/// <summary>
		/// Method that extends IsDefault functionality into all types
		/// </summary>
		/// <typeparam name="T">Any type</typeparam>
		/// <param name="source">Self instance of a generic type</param>
		/// <returns>Value that indicates if the instance has default value</returns>
		internal static bool IsDefault<T>(
			this T source
		)
		{
			return source.Equals(default(T));
		}
		/// <summary>
		/// Method that extends GetValue functionality into dictionaries
		/// </summary>
		/// <typeparam name="TKey">Dictionary key type</typeparam>
		/// <typeparam name="TValue">Dictionary value type</typeparam>
		/// <param name="source">Sefl instance od the dictionary</param>
		/// <param name="key">Dictionary key</param>
		/// <returns>Value found or the default value</returns>
		internal static TValue GetValue<TKey, TValue>(
			this Dictionary<TKey, TValue> source,
			TKey key
		)
		{
			TValue value = default(TValue);

			var success = source.TryGetValue(key, out value);

			return success ? value : Activator.CreateInstance<TValue>();
		}

		internal static void SetValue<TKey, TValue>(
			this Dictionary<TKey, TValue> source,
			TKey key,
			TValue value
		)
		{
			TValue dictionaryValue = default(TValue);

			var success = source.TryGetValue(key, out dictionaryValue);

			if (success)
			{
				source.Remove(key);
			}

			source.Add(key, value);
		}
		internal static string GetDescription<TEnum>(this TEnum source) where TEnum : Enum 
		{
			FieldInfo fi = source.GetType().GetField(source.ToString());

			DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return source.ToString();
		}
	}
}
