using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModelWrapper.Helpers
{
    /// <summary>
    /// Class that implements helpful methods for lambda expressions
    /// </summary>
    internal static class LambdaHelper
    {
        /// <summary>
        /// Method that generates a lambda expression for filter criteria
        /// </summary>
        /// <typeparam name="TSource">Expression type attribute</typeparam>
        /// <param name="filters">Dictionary of filters</param>
        /// <returns>Lambda expression for the filters</returns>
        internal static Expression<Func<TSource, bool>> GenerateFilterCriteriaExpression<TSource>(
            Dictionary<string,object> filters
        ) where TSource : class
        {
            List<Expression> expressions = new List<Expression>();

            var xExp = Expression.Parameter(typeof(TSource), "x");

            foreach (var filterProperty in filters)
            {
                var propertyParts = filterProperty.Key.Split("_");
                var property = typeof(TSource).GetProperties().Where(x=>x.Name.ToLower().Equals(propertyParts[0].ToLower())).SingleOrDefault();

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
        /// <summary>
        /// Method that generates a lambda expression for search criteria
        /// </summary>
        /// <typeparam name="TSource">Expression type attribute</typeparam>
        /// <param name="searchableProperties">List of searchable properties</param>
        /// <param name="terms">List of terms to search</param>
        /// <param name="queryStrict">parameter that indicates when all terms should be contained in the property</param>
        /// <returns>Lambda expression for the search</returns>
        internal static Expression<Func<TSource, bool>> GenerateSearchCriteriaExpression<TSource>(
            IList<string> searchableProperties,
            IList<string> terms,
            bool queryStrict)
            where TSource : class
        {
            List<Expression> orExpressions = new List<Expression>();

            var xExp = Expression.Parameter(typeof(TSource), "x");

            foreach (var propertyInfo in typeof(TSource).GetProperties().Where(x =>
                 searchableProperties.Any(y => y.ToLower().Equals(x.Name.ToLower()))
                 && !CriteriaHelper.GetNonQueryableTypes().Any(type=> type == x.PropertyType)
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

                foreach (var token in terms)
                {
                    andExpressions.Add(ExpressionHelper.GenerateStringContainsExpression(memberExp, Expression.Constant(token, typeof(string))));
                }
                orExpressions.Add(queryStrict ? ExpressionHelper.GenerateAndExpressions(andExpressions) : ExpressionHelper.GenerateOrExpression(andExpressions));
            }

            Expression orExp = ExpressionHelper.GenerateOrExpression(orExpressions);

            return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
        }
        /// <summary>
        /// Method that generates a lambda expression for select return
        /// </summary>
        /// <typeparam name="TSource">Expression type attribute</typeparam>
        /// <param name="selectProperties">List of properties that must be returned</param>
        /// <returns>Lambda expression for the select</returns>
        internal static Expression<Func<TSource, object>> GenerateSelectExpression<TSource>(
            IList<PropertyInfo> selectProperties
        ) where TSource : class
        {
            var source = Expression.Parameter(typeof(TSource), "x");

            var newType = ReflectionHelper.CreateNewType(selectProperties.ToList());

            var binding = selectProperties.ToList().Select(p => Expression.Bind(newType.GetProperty(p.Name), Expression.Property(source, p.Name))).ToList();
            var body = Expression.MemberInit(Expression.New(newType), binding);

            return Expression.Lambda<Func<TSource, object>>(body, source);
        }
        /// <summary>
        /// Method that return a list of 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static string GetPropertyName<TModel>(
            Expression<Func<TModel, object>> property
        ) where TModel : class
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
