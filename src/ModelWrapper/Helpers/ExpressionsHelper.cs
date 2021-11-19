using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for gererating expressions
    /// </summary>
    public static class ExpressionsHelper
    {
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
            if (comparisonType == "Not")
            {
                return Expression.Not(GenerateFilterStrictExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == "Contains")
            {
                return GenerateFilterContainsExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "NotContains")
            {
                return Expression.Not(GenerateFilterContainsExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == "StartsWith")
            {
                return GenerateFilterStartsWithExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "NotStartsWith")
            {
                return Expression.Not(GenerateFilterStartsWithExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == "EndsWith")
            {
                return GenerateFilterEndsWithExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "NotEndsWith")
            {
                return Expression.Not(GenerateFilterEndsWithExpression(memberExp, filterProperty, property));
            }
            if (comparisonType == "GreaterThan")
            {
                return GenerateFilterGreaterThanExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "GreaterThanOrEqual")
            {
                return GenerateFilterGreaterOrEqualExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "LessThan")
            {
                return GenerateFilterLessThanExpression(memberExp, filterProperty, property);
            }
            if (comparisonType == "LessThanOrEqual")
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
                    orExpressions.Add(Expression.Equal(memberExp, Expression.Constant(value, property.PropertyType)));
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.Equal(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
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
                    orExpressions.Add(GenerateStringContainsExpression(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(GenerateStringStartsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(GenerateStringEndsWithExpression(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(Expression.GreaterThan(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(Expression.GreaterThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(Expression.LessThan(memberExp, Expression.Constant(value, property.PropertyType)));
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
                    orExpressions.Add(Expression.LessThanOrEqual(memberExp, Expression.Constant(value, property.PropertyType)));
                }
                return GenerateOrExpression(orExpressions);
            }
            else
            {
                return Expression.LessThanOrEqual(memberExp, Expression.Constant(filterProperty.Value, property.PropertyType));
            }
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
            return Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "Contains", 1, 0, new List<Type> { typeof(string) }), tokenExp);
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
    }
}