using ModelWrapper.Binders;
using ModelWrapper.Extensions.Filter;
using ModelWrapper.Helpers;
using ModelWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ModelWrapper.Extensions.Aggregation
{
	public class Aggregation
	{
		public List<string> AggregateBy { get; set; }
		public List<AggregateProperty> Aggregates { get; set; }
	}
	public class AggregateProperty
	{
		public string Aggregate { get; set; }
		public List<string> Properties { get; set; }
	}
	public class AggregationResponse
	{
		public List<string> AggregateBy { get; set; }
		public List<AggregatePropertyResponse> Aggregates { get; set; }
	}
	public class AggregatePropertyResponse
	{
		public string Aggregate { get; set; }
		public Dictionary<string, object> Properties { get; set; }
	}

	public static class AggregationExtensions
	{
		/// <summary>
		/// Method that extends IWrapRequest<T> allowing to get filter properties from request
		/// </summary>
		/// <typeparam name="TModel">Generic type of the entity</typeparam>
		/// <param name="source">Self IWrapRequest<T> instance</param>
		/// <returns>Returns a dictionary with properties and values found</returns>
		internal static List<string> AggregatorProperties<TModel>(
			this WrapRequest<TModel> source
		) where TModel : class
		{
			var aggregatorProperties = new List<string>();

			foreach (var value in source.AllProperties
				.Where(x => !source.IsPropertySuppressed(x.Name))
				.Where(x => x.Name.ToLower().Equals(Constants.CONST_AGGREGATION_AGGREGATORS.ToLower()))
				.Select(x => x.Value)
				.ToList())
			{
				var property = typeof(TModel)
					.GetProperties()
					.Where(p => p.Name.ToLower().Equals(value.ToString().ToLower()))
					.SingleOrDefault();

				if (property != null)
				{
					aggregatorProperties.Add(property.Name);
				}
			}

			return aggregatorProperties;
		}
		public static void ClearAggregators<TModel>(
			this WrapRequest<TModel> source
		) where TModel : class
		{
			source.AllProperties.RemoveAll(property => property.Name.ToLower().Equals(Constants.CONST_AGGREGATION_AGGREGATORS.ToLower()));
		}
		public static void AddAggregator<TModel, TKey>(this WrapRequest<TModel> source, Expression<Func<TModel, TKey>> expression)
			where TModel : class
		{
			var propertyInfo = ExpressionsHelper.GetPropertyInfo(expression);

			if (propertyInfo != null)
			{
				var memberBinder = new WrapRequestMemberBinder(Constants.CONST_AGGREGATION_AGGREGATORS, WrapPropertySource.FromQuery, true);
				source.GetType().GetMethod("TrySetMember").Invoke(source, new object[] { memberBinder, propertyInfo.Name }); 
			}
		}
		/// <summary>
		/// Method that extends IWrapRequest<T> allowing to get filter properties from request
		/// </summary>
		/// <typeparam name="TModel">Generic type of the entity</typeparam>
		/// <param name="source">Self IWrapRequest<T> instance</param>
		/// <returns>Returns a dictionary with properties and values found</returns>
		internal static List<AggregateProperty> AggregateProperties<TModel>(
			this WrapRequest<TModel> source
		) where TModel : class
		{
			var aggregateProperties = new List<AggregateProperty>();
			foreach (var aggregate in AggregatesHelper.GetAggregates())
			{
				var values = new List<string>();
				foreach (var value in source.AllProperties
					.Where(x => !source.IsPropertySuppressed(x.Name))
					.Where(x => x.Name.ToLower().Equals(AggregatesHelper.AggregateWithType(aggregate).ToLower()))
					.Select(x => x.Value)
					.ToList())
				{
					var property = typeof(TModel)
						.GetProperties()
						.Where(p => p.Name.ToLower().Equals(value.ToString().ToLower()))
						.SingleOrDefault();

					if (property != null)
					{
						if(aggregate == AggregatesHelper.Aggregates.Sum || aggregate == AggregatesHelper.Aggregates.Avg)
						{
							if(AggregatesHelper.NumericTypes.Any(t => t == property.PropertyType))
							{
								values.Add(property.Name);
							}
						}
						else
							values.Add(property.Name);
					}
				}

				if (values.Count > 0)
				{
					aggregateProperties.Add(new AggregateProperty()
					{
						Aggregate = AggregatesHelper.AggregateType(aggregate),
						Properties = values
					});
				}
			}

			return aggregateProperties;
		}
		public static void ClearAggregates<TModel>(
			this WrapRequest<TModel> source
		) where TModel : class
		{
			foreach (var aggregate in AggregatesHelper.GetAggregates())
				source.AllProperties.RemoveAll(property => property.Name.ToLower().Equals(AggregatesHelper.AggregateWithType(aggregate).ToLower()));
		}
		public static void AddAggregate<TModel, TKey>(this WrapRequest<TModel> source, AggregatesHelper.Aggregates aggregate, Expression<Func<TModel, TKey>> expression)
			where TModel : class
		{
			var propertyInfo = ExpressionsHelper.GetPropertyInfo(expression);

			if (propertyInfo != null)
			{
				var memberBinder = new WrapRequestMemberBinder(AggregatesHelper.AggregateWithType(aggregate), WrapPropertySource.FromQuery, true);
				source.GetType().GetMethod("TrySetMember").Invoke(source, new object[] { memberBinder, propertyInfo.Name });
			}
		}

		/// <summary>
		/// Method that extends IQueryable<T> allowing to filter query with request properties
		/// </summary>
		/// <typeparam name="TSource">Generic type of the entity</typeparam>
		/// <param name="source">Self IQueryable<T> instance</param>
		/// <param name="request">Self IWrapRequest<T> instance</param>
		/// <returns>Returns IQueryable instance with with the configuration for filter</returns>
		public static IQueryable<TSource> Aggregate<TSource>(
			this IQueryable<TSource> source,
			WrapRequest<TSource> request
		) where TSource : class
		{
			var aggregatorProperties = request.AggregatorProperties();
			var aggregateProperties = request.AggregateProperties();
			if (aggregatorProperties.Count > 0 || aggregateProperties.Count > 0)
			{
				request.RequestObject.SetValue(Constants.CONST_AGGREGATION.ToCamelCase(), new Aggregation()
				{
					AggregateBy = aggregatorProperties,
					Aggregates = aggregateProperties
				});

				var aggregationResponse = new Dictionary<string, object>();

				if (aggregatorProperties.Count > 0)
				{
					aggregationResponse.Add("Aggregators".ToCamelCase(), aggregatorProperties);
				}

				foreach (var aggregateProperty in aggregateProperties)
				{					
					if (aggregateProperty.Aggregate == AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Sum))
					{
						var aggregateSum = new Dictionary<string, object>();
						foreach (var property in aggregateProperty.Properties)
						{
							Type type = typeof(TSource);

							var lambda = LambdasHelper.GenerateAccessMemberExpression<TSource>(ref type, "x", property);

							var method = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "Sum", 2, 1, type);

							var sumValue = method.MakeGenericMethod(typeof(TSource)).Invoke(null, new object[] { source, lambda });

							if (sumValue != null)
							{
								aggregateSum.Add(property, sumValue);
							}
						}

						aggregationResponse.Add(AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Sum), aggregateSum);
					}
					if (aggregateProperty.Aggregate == AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Avg))
					{
						var aggregateAvg = new Dictionary<string, object>();
						foreach (var property in aggregateProperty.Properties)
						{
							Type type = typeof(TSource);

							var lambda = LambdasHelper.GenerateAccessMemberExpression<TSource>(ref type, "x", property);

							var method = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "Average", 2, 1, type);

							var avgValue = method.MakeGenericMethod(typeof(TSource)).Invoke(null, new object[] { source, lambda });

							if (avgValue != null)
							{
								aggregateAvg.Add(property, avgValue);
							}
						}

						aggregationResponse.Add(AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Avg), aggregateAvg);
					}
					if (aggregateProperty.Aggregate == AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Min))
					{
						var aggregateMin = new Dictionary<string, object>();
						foreach (var property in aggregateProperty.Properties)
						{
							Type type = typeof(TSource);

							var lambda = LambdasHelper.GenerateAccessMemberExpression<TSource>(ref type, "x", property);

							var method = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "Min", 2, 2);

							var minValue = method.MakeGenericMethod(typeof(TSource), type).Invoke(null, new object[] { source, lambda });

							if (minValue != null)
							{
								aggregateMin.Add(property, minValue);
							}
						}

						aggregationResponse.Add(AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Min), aggregateMin);
					}
					if (aggregateProperty.Aggregate == AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Max))
					{
						var aggregateMax = new Dictionary<string, object>();
						foreach (var property in aggregateProperty.Properties)
						{
							Type type = typeof(TSource);

							var lambda = LambdasHelper.GenerateAccessMemberExpression<TSource>(ref type, "x", property);

							var method = ReflectionsHelper.GetMethodFromType(typeof(Queryable), "Max", 2, 2);

							var maxValue = method.MakeGenericMethod(typeof(TSource), type).Invoke(null, new object[] { source, lambda });

							if (maxValue != null)
							{
								aggregateMax.Add(property, maxValue);
							}
						}

						aggregationResponse.Add(AggregatesHelper.AggregateType(AggregatesHelper.Aggregates.Max), aggregateMax);
					}
				}

				request.RequestObject.SetValue("AggregationResponse".ToCamelCase(), aggregationResponse);
			}

			return source;
		}
	}
}
