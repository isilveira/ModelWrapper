using ModelWrapper.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace ModelWrapper.Helpers
{
	public static class AggregatesHelper
	{
		internal static string AggregateWithType(Aggregates aggregate) => $"Aggregate{aggregate.GetDescription()}";
		internal static string AggregateType(Aggregates aggregate) => $"{aggregate.GetDescription()}";
		public enum Aggregates
		{
			[Description("Min")] Min,
			[Description("Max")] Max,
			[Description("Sum")] Sum,
			[Description("Avg")] Avg,
		}
		internal static List<Aggregates> GetAggregates()
		{
			var aggregates = new List<Aggregates>()
			{
				Aggregates.Min,
				Aggregates.Max,
				Aggregates.Sum,
				Aggregates.Avg
			};

			return aggregates;
		}
		internal static List<string> GetAggregateTypes()
		{
			return GetAggregates().Select(a => AggregateType(a)).Distinct().ToList();
		}
		internal static List<string> GetAggregatesWithType()
		{
			return GetAggregates().Select(a => AggregateWithType(a)).Distinct().ToList();
		}
		internal static List<Type> NumericTypes => new List<Type>
		{
			typeof(int), typeof(int?),
			typeof(long), typeof(long?),
			typeof(float), typeof(float?),
			typeof(double), typeof(double?),
			typeof(decimal), typeof(decimal?),
		};
	}
}
