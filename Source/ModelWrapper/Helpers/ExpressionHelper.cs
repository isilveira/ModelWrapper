using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace ModelWrapper.Helpers
{
    public static class ExpressionHelper
    {
        internal static Expression GenerateFilterComparationExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property, string comparation = null)
        {
            if (string.IsNullOrWhiteSpace(comparation))
            {
                return GenerateFilterStrictExpression(memberExp, filterProperty, property);
            }
            if (comparation == "Not")
            {
                return Expression.Not(GenerateFilterStrictExpression(memberExp, filterProperty, property));
            }
            if (comparation == "Contains")
            {
                return GenerateFilterContainsExpression(memberExp, filterProperty, property);
            }
            if (comparation == "NotContains")
            {
                return Expression.Not(GenerateFilterContainsExpression(memberExp, filterProperty, property));
            }
            if (comparation == "StartsWith")
            {
                return GenerateFilterStartsWithExpression(memberExp, filterProperty, property);
            }
            if (comparation == "NotStartsWith")
            {
                return Expression.Not(GenerateFilterStartsWithExpression(memberExp, filterProperty, property));
            }
            if (comparation == "EndsWith")
            {
                return GenerateFilterEndsWithExpression(memberExp, filterProperty, property);
            }
            if (comparation == "NotEndsWith")
            {
                return Expression.Not(GenerateFilterEndsWithExpression(memberExp, filterProperty, property));
            }
            if (comparation == "GreaterThan")
            {
                return GenerateFilterGreaterThanExpression(memberExp, filterProperty, property);
            }
            if (comparation == "GreaterThanOrEqual")
            {
                return GenerateFilterGreaterEqualExpression(memberExp, filterProperty, property);
            }
            if (comparation == "LessThan")
            {
                return GenerateFilterSmallerThanExpression(memberExp, filterProperty, property);
            }
            if (comparation == "LessThanOrEqual")
            {
                return GenerateFilterSmallerEqualExpression(memberExp, filterProperty, property);
            }
            return Expression.Empty();
        }
        internal static Expression GenerateFilterStrictExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterContainsExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterStartsWithExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterEndsWithExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterGreaterThanExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterGreaterEqualExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterSmallerThanExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateFilterSmallerEqualExpression(Expression memberExp, KeyValuePair<string, object> filterProperty, PropertyInfo property)
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
        internal static Expression GenerateStringContainsExpression(Expression memberExp, ConstantExpression tokenExp)
        {
            return Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "Contains", 1, 0, new List<Type> { typeof(string) }), tokenExp);
        }
        internal static Expression GenerateStringStartsWithExpression(Expression memberExp, ConstantExpression tokenExp)
        {
            return Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "StartsWith", 1, 0, new List<Type> { typeof(string) }), tokenExp);
        }
        internal static Expression GenerateStringEndsWithExpression(Expression memberExp, ConstantExpression tokenExp)
        {
            return Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "EndsWith", 1, 0, new List<Type> { typeof(string) }), tokenExp);
        }
        internal static Expression GenerateAndExpressions(List<Expression> expressions)
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
        internal static Expression GenerateOrExpression(List<Expression> expressions)
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
        internal static Expression GenereteLambdaExpression<T>(ref Type type, string propertyName)
        {
            ParameterExpression arg = Expression.Parameter(type, "x");
            List<string> listProperties = propertyName.Split('.').ToList();

            Expression expr = arg;
            foreach (string item in listProperties)
            {
                PropertyInfo np = type.GetProperties().SingleOrDefault(x => x.Name.ToLower() == item.ToLower());
                expr = Expression.MakeMemberAccess(expr, np);
                type = np.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            return Expression.Lambda(delegateType, expr, arg);
        }
    }
}
