using ModelWrapper.Extensions;
using ModelWrapper.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for gererating expressions
    /// </summary>
    public static class ExpressionsHelper
    {
        internal static string GetPropertyComparisonType(string propertyName)
		{
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.NotEqual)))
			{
                return CriteriasHelper.Criterias.NotEqual.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.In)))
			{
				return CriteriasHelper.Criterias.In.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.NotIn)))
			{
				return CriteriasHelper.Criterias.NotIn.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.Contains)))
			{
                return CriteriasHelper.Criterias.Contains.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.NotContains)))
			{
                return CriteriasHelper.Criterias.NotContains.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.StartsWith)))
			{
                return CriteriasHelper.Criterias.StartsWith.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.NotStartsWith)))
			{
				return CriteriasHelper.Criterias.NotStartsWith.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.EndsWith)))
			{
				return CriteriasHelper.Criterias.EndsWith.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.NotEndsWith)))
			{
				return CriteriasHelper.Criterias.NotEndsWith.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.GreaterThan)))
			{
				return CriteriasHelper.Criterias.GreaterThan.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.GreaterThanOrEqual)))
			{
				return CriteriasHelper.Criterias.GreaterThanOrEqual.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.LessThan)))
			{
				return CriteriasHelper.Criterias.LessThan.GetDescription();
			}
			if (propertyName.EndsWith(CriteriasHelper.CriteriaWithSeparator(CriteriasHelper.Criterias.LessThanOrEqual)))
			{
				return CriteriasHelper.Criterias.LessThanOrEqual.GetDescription();
			}
			return CriteriasHelper.Criterias.Equal.GetDescription();
		}
        /// <summary>
        /// Method that generate filter comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <param name="comparisonType">Comparison Type</param>
        /// <returns>Expression for the filter comparison</returns>
        internal static Expression GenerateFilterComparisonExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property,
            string comparisonType = null
        )
        {
            if (string.IsNullOrWhiteSpace(comparisonType))
            {
                return GenerateFilterStrictExpression(memberExp, filterProperty, property);
			}
			if (comparisonType == CriteriasHelper.Criterias.NotEqual.GetDescription())
            {
                return Expression.Not(GenerateFilterStrictExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == CriteriasHelper.Criterias.In.GetDescription())
			{
				return GenerateFilterInExpression(memberExp, filterProperty, property);
			}
			if (comparisonType == CriteriasHelper.Criterias.NotIn.GetDescription())
			{
				return Expression.Not(GenerateFilterInExpression(memberExp, filterProperty, property));
			}
			if (comparisonType == CriteriasHelper.Criterias.Contains.GetDescription())
            {
                return GenerateFilterContainsExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.NotContains.GetDescription())
            {
                return Expression.Not(GenerateFilterContainsExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == CriteriasHelper.Criterias.StartsWith.GetDescription())
            {
                return GenerateFilterStartsWithExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.NotStartsWith.GetDescription())
            {
                return Expression.Not(GenerateFilterStartsWithExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == CriteriasHelper.Criterias.EndsWith.GetDescription())
            {
                return GenerateFilterEndsWithExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.NotEndsWith.GetDescription())
            {
                return Expression.Not(GenerateFilterEndsWithExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == CriteriasHelper.Criterias.GreaterThan.GetDescription())
            {
                return GenerateFilterGreaterThanExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.GreaterThanOrEqual.GetDescription())
            {
                return GenerateFilterGreaterOrEqualExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.LessThan.GetDescription())
            {
                return GenerateFilterLessThanExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == CriteriasHelper.Criterias.LessThanOrEqual.GetDescription())
            {
                return GenerateFilterLessThanOrEqualExpression(memberExp, filterProperty, property);
            }
            return Expression.Empty();
        }
        /// <summary>
        /// Method that generate filter strict comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter strict comparison</returns>
        internal static Expression GenerateFilterStrictExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value != null && filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                    {
                        List<Expression> subAndExpressions = new List<Expression>();
                        Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

                        subAndExpressions.Add(memberHasValue);
                        subAndExpressions.Add(Expression.Equal(memberExp, Expression.Constant(value, property.PropertyType)));
                        orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
                    }
                    else
                    {
						orExpressions.Add(Expression.Equal(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.Equal(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
		}
		/// <summary>
		/// Method that generate filter in comparison expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter in comparison</returns>
		internal static Expression GenerateFilterInExpression(
			Expression memberExp,
			KeyValuePair<string, object> filterProperty,
			PropertyInfo property
		)
		{
            if(filterProperty.Value.GetType() != typeof(string) && filterProperty.Value.GetType() == memberExp.Type)
            {
                var typedList = memberExp.Type.CreateGenericList();

				try
				{
                    typedList.Add(Convert.ChangeType(filterProperty.Value, memberExp.Type));
				}
				catch (Exception ex)
				{
					throw new Exception($"Cannot convert value '{filterProperty.Value}' to type '{memberExp.Type.Name}' of property {memberExp}", ex);
				}

				return GenerateListContainsExpression(memberExp, Expression.Constant(typedList, typedList.GetType()), typedList.GetType());
			}

			if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
			{
				List<Expression> orExpressions = new List<Expression>();
				foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(GenerateListContainsExpression(memberExp, Expression.Constant(value, property.PropertyType), property.PropertyType));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(GenerateListContainsExpression(memberExp, Expression.Constant(value, property.PropertyType), property.PropertyType));
					}
				}
				return GenerateOrExpression(orExpressions);
			}
			else
			{
                if(filterProperty.Value.GetType() == typeof(string) && ((string)filterProperty.Value).Split(ConfigurationService.GetConfiguration().DefaultInStringSeparator).Length >= 1)
                {
                    var listString = ((string)filterProperty.Value).Split(ConfigurationService.GetConfiguration().DefaultInStringSeparator).ToList();
                    var typedList = listString.Select(item => {
                        try
                        {
                            return Convert.ChangeType(item, memberExp.Type);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Cannot convert value '{item}' to type '{memberExp.Type.Name}' of property {memberExp}", ex);
                        }
                    }).ToList();

					return GenerateListContainsExpression(memberExp, Expression.Constant(typedList, typedList.GetType()), typedList.GetType());
				}

				return GenerateListContainsExpression(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType), property.PropertyType);
			}
		}
		/// <summary>
		/// Method that generate filter contains comparison expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter contains comparison</returns>
		internal static Expression GenerateFilterContainsExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(GenerateStringContainsExpression(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(GenerateStringContainsExpression(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return GenerateStringContainsExpression(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter starts with comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter starts with comparison</returns>
        internal static Expression GenerateFilterStartsWithExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(GenerateStringStartsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(GenerateStringStartsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return GenerateStringStartsWithExpression(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter ends with comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter ends with comparison</returns>
        internal static Expression GenerateFilterEndsWithExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(GenerateStringEndsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(GenerateStringEndsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return GenerateStringEndsWithExpression(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter greater than comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter greater than comparison</returns>
        internal static Expression GenerateFilterGreaterThanExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(Expression.GreaterThan(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(Expression.GreaterThan(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.GreaterThan(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter greater than or equal comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter greater than or equal comparison</returns>
        internal static Expression GenerateFilterGreaterOrEqualExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(Expression.GreaterThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(Expression.GreaterThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.GreaterThanOrEqual(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter less than comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter less than comparison</returns>
        internal static Expression GenerateFilterLessThanExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(Expression.LessThan(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(Expression.LessThan(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.LessThan(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
        }
        /// <summary>
        /// Method that generate filter less than or equal comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter less than or equal comparison</returns>
        internal static Expression GenerateFilterLessThanOrEqualExpression(
            Expression memberExp,
            KeyValuePair<string, object> filterProperty,
            PropertyInfo property
        )
        {
            if (filterProperty.Value.GetType().IsGenericType && filterProperty.Value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                List<Expression> orExpressions = new List<Expression>();
                foreach (var value in ((List<object>)filterProperty.Value))
				{
					if (Nullable.GetUnderlyingType(property.PropertyType) != null)
					{
						List<Expression> subAndExpressions = new List<Expression>();
						Expression memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));

						subAndExpressions.Add(memberHasValue);
						subAndExpressions.Add(Expression.LessThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
						orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
					}
					else
					{
						orExpressions.Add(Expression.LessThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
					}
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.LessThanOrEqual(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
		}
		/// <summary>
		/// Method that generate filter list contains comparison expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter list contains comparison</returns>
		internal static Expression GenerateListContainsExpression(
			Expression memberExp,
			ConstantExpression tokenExp,
            Type listType
		)
		{
			var method = ReflectionsHelper.GetMethodFromType(listType, "Contains", 1, 0, new List<Type> { memberExp.Type });

			return Expression.Call(tokenExp, method, memberExp);
		}
		/// <summary>
		/// Method that generate filter string contains comparison expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter string contains comparison</returns>
		internal static Expression GenerateStringContainsExpression(
            Expression memberExp,
            ConstantExpression tokenExp
        )
        {
            var method = ReflectionsHelper.GetMethodFromType(memberExp.Type, "Contains", 1, 0, new List<Type> { typeof(string) });

            return Expression.Call(memberExp, method, tokenExp);
        }
        /// <summary>
        /// Method that generate filter string starts with comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter string starts with comparison</returns>
        internal static Expression GenerateStringStartsWithExpression(
            Expression memberExp,
            ConstantExpression tokenExp
        )
        {
            return Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "StartsWith", 1, 0, new List<Type> { typeof(string) }), tokenExp);
        }
        /// <summary>
        /// Method that generate filter string ends with comparison expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter string ends with comparison</returns>
        internal static Expression GenerateStringEndsWithExpression(
            Expression memberExp,
            ConstantExpression tokenExp
        )
        {
            return Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "EndsWith", 1, 0, new List<Type> { typeof(string) }), tokenExp);
        }
        /// <summary>
        /// Method that generate filter And expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter And</returns>
        internal static Expression GenerateAndExpressions(
            List<Expression> expressions
        )
        {
            Expression andExp = Expression.Empty();
            if (expressions.Count == 1)
            {
                andExp = expressions[0];
            }
            else
            {
                andExp = Expression.And(expressions[0], expressions[1]);

                for (int i = 2; i < expressions.Count; i++)
                {
                    andExp = Expression.And(andExp, expressions[i]);
                }
            }
            return andExp;
        }
        /// <summary>
        /// Method that generate filter Or expression
        /// </summary>
        /// <param name="memberExp">Member expression</param>
        /// <param name="filterProperty">KeyValuePair for properties and values</param>
        /// <param name="property">Property that will be filtered</param>
        /// <returns>Expression for the filter Or</returns>
        internal static Expression GenerateOrExpression(
            List<Expression> expressions
        )
        {
            Expression orExp = Expression.Empty();
            if (expressions.Count == 1)
            {
                orExp = expressions[0];
            }
            else
            {
                orExp = Expression.Or(expressions[0], expressions[1]);

                for (int i = 2; i < expressions.Count; i++)
                {
                    orExp = Expression.Or(orExp, expressions[i]);
                }
            }
            return orExp;
		}
		/// <summary>
		/// Method that generate filter And expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter AndAlso</returns>
		internal static Expression GenerateAndAlsoExpressions(
			List<Expression> expressions
		)
		{
			Expression andExp = Expression.Empty();
			if (expressions.Count == 1)
			{
				andExp = expressions[0];
			}
			else
			{
				andExp = Expression.AndAlso(expressions[0], expressions[1]);

				for (int i = 2; i < expressions.Count; i++)
				{
					andExp = Expression.AndAlso(andExp, expressions[i]);
				}
			}
			return andExp;
		}
		/// <summary>
		/// Method that generate filter Or expression
		/// </summary>
		/// <param name="memberExp">Member expression</param>
		/// <param name="filterProperty">KeyValuePair for properties and values</param>
		/// <param name="property">Property that will be filtered</param>
		/// <returns>Expression for the filter OrElse</returns>
		internal static Expression GenerateOrElseExpression(
			List<Expression> expressions
		)
		{
			Expression orExp = Expression.Empty();
			if (expressions.Count == 1)
			{
				orExp = expressions[0];
			}
			else
			{
				orExp = Expression.OrElse(expressions[0], expressions[1]);

				for (int i = 2; i < expressions.Count; i++)
				{
					orExp = Expression.OrElse(orExp, expressions[i]);
				}
			}
			return orExp;
		}
	}
}