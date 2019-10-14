﻿using ModelWrapper.Extensions;
using ModelWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ModelWrapper.Helpers
{
    internal static class LambdaHelper
    {
        internal static Expression<Func<TSource, bool>> GenerateFilterCriteriaExpression<TSource>(Dictionary<string,object> filters) where TSource : class
        {
            List<Expression> expressions = new List<Expression>();

            var xExp = Expression.Parameter(typeof(TSource), "x");

            foreach (var filterProperty in filters)
            {
                var propertyParts = filterProperty.Key.Split("_");
                var property = typeof(TSource).GetProperty(propertyParts[0]);

                Expression memberExp = Expression.MakeMemberAccess(xExp, property);

                if (propertyParts.Count() == 1)
                {
                    expressions.Add(ExpressionHelper.GenerateFilterComparationExpression(memberExp, filterProperty, property));
                }
                else
                {
                    expressions.Add(ExpressionHelper.GenerateFilterComparationExpression(memberExp, filterProperty, property, propertyParts[1]));
                }
            }

            Expression orExp = ExpressionHelper.GenerateAndExpressions(expressions);

            return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
        }
        internal static Expression<Func<TSource, bool>> GenerateSearchCriteriaExpression<TSource>(
            IList<string> searchableProperties,
            IList<string> tokens,
            bool queryStrict)
            where TSource : class
        {
            List<Expression> orExpressions = new List<Expression>();

            var xExp = Expression.Parameter(typeof(TSource), "x");

            foreach (var propertyInfo in typeof(TSource).GetProperties().Where(x =>
                 searchableProperties.Any(y => y.ToLower().Equals(x.Name.ToLower()))
            ).ToList())
            {
                Expression memberExp = Expression.MakeMemberAccess(xExp, propertyInfo);
                Expression memberHasValue = null;
                if (memberExp.Type != typeof(string))
                {
                    if (Nullable.GetUnderlyingType(memberExp.Type) != null)
                    {
                        memberHasValue = Expression.MakeMemberAccess(memberExp, memberExp.Type.GetProperty("HasValue"));
                        memberHasValue = queryStrict ? memberHasValue : Expression.Not(memberHasValue);
                    }
                    else if (memberExp.Type.IsClass)
                    {
                        memberHasValue = Expression.Equal(memberExp, Expression.Constant(null));
                        memberHasValue = !queryStrict ? memberHasValue : Expression.Not(memberHasValue);
                    }
                    memberExp = Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "ToString", 0, 0));
                }
                else
                {
                    memberHasValue = Expression.Equal(memberExp, Expression.Constant(null));
                    memberHasValue = !queryStrict ? memberHasValue : Expression.Not(memberHasValue);
                }

                memberExp = Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "ToLower", 0, 0));
                List<Expression> andExpressions = new List<Expression>();

                foreach (var token in tokens)
                {
                    andExpressions.Add(ExpressionHelper.GenerateStringContainsExpression(memberExp, Expression.Constant(token, typeof(string))));
                }
                orExpressions.Add(queryStrict ? ExpressionHelper.GenerateAndExpressions(andExpressions) : ExpressionHelper.GenerateOrExpression(andExpressions));
            }

            Expression orExp = ExpressionHelper.GenerateOrExpression(orExpressions);

            return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
        }
        internal static Expression<Func<TSource, object>> GenerateSelectExpression<TSource>(IList<PropertyInfo> selectProperties) where TSource : class
        {
            var source = Expression.Parameter(typeof(TSource), "x");

            var newType = ReflectionHelper.CreateNewType(selectProperties.ToList());

            var binding = selectProperties.ToList().Select(p => Expression.Bind(newType.GetProperty(p.Name), Expression.Property(source, p.Name))).ToList();
            var body = Expression.MemberInit(Expression.New(newType), binding);

            return Expression.Lambda<Func<TSource, object>>(body, source);
        }
        internal static string GetPropertyName<TModel>(Expression<Func<TModel, object>> property) where TModel : class
        {
            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return ((PropertyInfo)memberExpression.Member).Name;
        }
    }
}
