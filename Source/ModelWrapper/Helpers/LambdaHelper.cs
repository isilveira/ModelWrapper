using ModelWrapper.Extensions;
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
        private static Expression<Func<TSource, bool>> GenerateFilterCriteriaExpression<TSource>(IWrapRequest<TSource> filter) where TSource : class
        {
            //List<Expression> expressions = new List<Expression>();

            //var xExp = Expression.Parameter(typeof(TSource), "x");

            //foreach (var filterProperty in filter.FilterProperties)
            //{
            //    var propertyParts = filterProperty.Key.Split("_");
            //    var property = typeof(TSource).GetProperty(propertyParts[0]);

            //    Expression memberExp = Expression.MakeMemberAccess(xExp, property);

            //    if (propertyParts.Count() == 1)
            //    {
            //        expressions.Add(ExpressionsHelper.GenerateFilterComparationExpression(memberExp, filterProperty, property));
            //    }
            //    else
            //    {
            //        expressions.Add(ExpressionsHelper.GenerateFilterComparationExpression(memberExp, filterProperty, property, propertyParts[1]));
            //    }
            //}

            //Expression orExp = ExpressionsHelper.GenerateAndExpressions(expressions);

            //return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
            throw new NotImplementedException();
        }
        private static Expression<Func<TSource, bool>> GenerateSearchCriteriaExpression<TSource>(IList<string> tokens, IWrapRequest<TSource> filter)
            where TSource : class
        {
            //List<Expression> orExpressions = new List<Expression>();

            //var xExp = Expression.Parameter(typeof(TSource), "x");

            //foreach (var propertyInfo in ReflectionHelper.GetPropertiesFromType(filter.GetSearchableProperties(typeof(TSource).GetProperties()), filter.QueryProperties))
            //{
            //    Expression memberExp = Expression.MakeMemberAccess(xExp, propertyInfo);
            //    Expression memberHasValue = null;
            //    if (memberExp.Type != typeof(string))
            //    {
            //        if (Nullable.GetUnderlyingType(memberExp.Type) != null)
            //        {
            //            memberHasValue = Expression.MakeMemberAccess(memberExp, memberExp.Type.GetProperty("HasValue"));
            //            memberHasValue = filter.QueryStrict ? memberHasValue : Expression.Not(memberHasValue);
            //        }
            //        else if (memberExp.Type.IsClass)
            //        {
            //            memberHasValue = Expression.Equal(memberExp, Expression.Constant(null));
            //            memberHasValue = !filter.QueryStrict ? memberHasValue : Expression.Not(memberHasValue);
            //        }
            //        memberExp = Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "ToString", 0, 0));
            //    }
            //    else
            //    {
            //        memberHasValue = Expression.Equal(memberExp, Expression.Constant(null));
            //        memberHasValue = !filter.QueryStrict ? memberHasValue : Expression.Not(memberHasValue);
            //    }

            //    memberExp = Expression.Call(memberExp, ReflectionHelper.GetMethodFromType(memberExp.Type, "ToLower", 0, 0));
            //    List<Expression> andExpressions = new List<Expression>();

            //    foreach (var token in tokens)
            //    {
            //        andExpressions.Add(ExpressionsHelper.GenerateStringContainsExpression(memberExp, Expression.Constant(token, typeof(string))));
            //    }
            //    orExpressions.Add(filter.QueryStrict ? ExpressionsHelper.GenerateAndExpressions(andExpressions) : ExpressionsHelper.GenerateOrExpression(andExpressions));
            //}

            //Expression orExp = ExpressionsHelper.GenerateOrExpression(orExpressions);

            //return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
            throw new NotImplementedException();
        }
        internal static Expression<Func<TSource, object>> GenerateSelectExpression<TSource>(IWrapRequest<TSource> request) where TSource : class
        {
            var source = Expression.Parameter(typeof(TSource), "x");

            var properties = typeof(TSource).GetProperties().Where(x => request.ResponseProperties().Any(y => y == x.Name)).ToList();

            var newType = ReflectionHelper.CreateNewType(properties);

            var binding = properties.ToList().Select(p => Expression.Bind(newType.GetProperty(p.Name), Expression.Property(source, p.Name))).ToList();
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
