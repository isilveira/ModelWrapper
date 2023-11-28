using ModelWrapper.Models;
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
    internal static class LambdasHelper
    {
        /// <summary>
        /// Method that generates a lambda expression for filter criteria
        /// </summary>
        /// <typeparam name="TSource">Expression type attribute</typeparam>
        /// <param name="filters">Dictionary of filters</param>
        /// <returns>Lambda expression for the filters</returns>
        internal static Expression<Func<TSource, bool>> GenerateFilterCriteriaExpression<TSource>(
            Dictionary<string, object> filters
        ) where TSource : class
        {
            List<Expression> expressions = new List<Expression>();

            var xExp = Expression.Parameter(typeof(TSource), "x");

            foreach (var filterProperty in filters)
            {
                var propertyParts = filterProperty.Key.Split('_');
                var property = typeof(TSource).GetProperties().Where(x => x.Name.ToLower().Equals(propertyParts[0].ToLower())).SingleOrDefault();

                Expression memberExp = Expression.MakeMemberAccess(xExp, property);

                if (propertyParts.Count() == 1)
                {
                    expressions.Add(ExpressionsHelper.GenerateFilterComparisonExpression(memberExp, filterProperty, property));
                }
                else
                {
                    expressions.Add(ExpressionsHelper.GenerateFilterComparisonExpression(memberExp, filterProperty, property, propertyParts[1]));
                }
            }

            Expression orExp = ExpressionsHelper.GenerateAndExpressions(expressions);

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
                 && !CriteriasHelper.GetNonQueryableTypes().Any(type => type == x.PropertyType)
            ).ToList())
			{
				SetPropertyExpressions(terms, queryStrict, orExpressions, xExp, propertyInfo);
			}

			Expression orExp = ExpressionsHelper.GenerateOrElseExpression(orExpressions);

            return Expression.Lambda<Func<TSource, bool>>(orExp.Reduce(), xExp);
		}

		private static void SetPropertyExpressions(IList<string> terms, bool queryStrict, List<Expression> orExpressions, Expression xExp, PropertyInfo propertyInfo)
		{
			Expression memberExp = Expression.MakeMemberAccess(xExp, propertyInfo);
			Expression memberHasValue = null;
			if (memberExp.Type != typeof(string))
			{
				if (Nullable.GetUnderlyingType(memberExp.Type) != null)
				{
					memberHasValue = Expression.MakeMemberAccess(memberExp, memberExp.Type.GetProperty("HasValue"));
					memberHasValue = queryStrict ? memberHasValue : Expression.Not(memberHasValue);
					memberExp = Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "ToString", 0, 0));
				}
				else if (memberExp.Type.IsClass)
				{
					List<Expression> subAndExpressions = new List<Expression>();
					List<Expression> subOrExpressions = new List<Expression>();
					memberHasValue = Expression.NotEqual(memberExp, Expression.Constant(null));
                    
                    subAndExpressions.Add(memberHasValue);
					
                    foreach (var subPropertyInfo in memberExp.Type.GetProperties().Where(x =>
						 !TypesHelper.TypeIsComplex(x.PropertyType)
						 && !CriteriasHelper.GetNonQueryableTypes().Any(type => type == x.PropertyType)
					).ToList())
					{
						SetPropertyExpressions(terms, queryStrict, subOrExpressions, memberExp, subPropertyInfo);
					}

                    subAndExpressions.Add(ExpressionsHelper.GenerateOrElseExpression(subOrExpressions));

                    orExpressions.Add(ExpressionsHelper.GenerateAndAlsoExpressions(subAndExpressions));
                    return;
                }
                else
                {
                    memberExp = Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "ToString", 0, 0));
                }
			}
			else
			{
				memberHasValue = Expression.Equal(memberExp, Expression.Constant(null));
				memberHasValue = !queryStrict ? memberHasValue : Expression.Not(memberHasValue);
			}

			memberExp = Expression.Call(memberExp, ReflectionsHelper.GetMethodFromType(memberExp.Type, "ToLower", 0, 0));
			List<Expression> andExpressions = new List<Expression>();

			foreach (var token in terms)
			{
				andExpressions.Add(ExpressionsHelper.GenerateStringContainsExpression(memberExp, Expression.Constant(token, typeof(string))));
			}
			orExpressions.Add(queryStrict ? ExpressionsHelper.GenerateAndAlsoExpressions(andExpressions) : ExpressionsHelper.GenerateOrElseExpression(andExpressions));
		}
		/// <summary>
		/// Method that generates a lambda expression for select return
		/// </summary>
		/// <typeparam name="TSource">Expression type attribute</typeparam>
		/// <param name="selectedModel">Object that represents a format to the returned object</param>
		/// <returns>Lambda expression for the select</returns>
		internal static Expression<Func<TSource, object>> GenerateSelectExpression<TSource>(
            SelectedModel selectedModel
        ) where TSource : class
        {
            var source = Expression.Parameter(typeof(TSource), "x");

            var newType = ReflectionsHelper.CreateNewType(selectedModel);

            var body = GenerateNewBody(selectedModel, source, newType);

            return Expression.Lambda<Func<TSource, object>>(body, source);
        }

        private static ConditionalExpression GenerateNewBody(SelectedModel selectedModel, Expression source, Type newType, int level = 0)
        {
            var binding = new List<MemberAssignment>();
            foreach (var property in selectedModel.Properties)
            {
                if (TypesHelper.TypeIsComplex(property.OriginalType) && TypesHelper.TypeIsCollection(property.OriginalType))
                {
                    var newPropertyInfo = newType.GetProperty(property.RequestedName);
                    var collectionArgumentType = newPropertyInfo.PropertyType.GetGenericArguments()[0];
                    var newSource = Expression.Parameter(TypesHelper.GetEntityTypeFromComplex(property.OriginalType), "x" + level.ToString());
                    var newBody = GenerateNewBody(property, newSource, collectionArgumentType, level++);
                    var selectOriginProperty = Expression.Property(source, property.Name);

                    var funcType = typeof(Func<,>).MakeGenericType(TypesHelper.GetEntityTypeFromComplex(property.OriginalType), collectionArgumentType);
                    var selectLambdaExpression = Expression.Lambda(funcType, newBody, newSource);
                    var memberAssignment = Expression.Call(typeof(Enumerable), "Select", new Type[] { TypesHelper.GetEntityTypeFromComplex(property.OriginalType), collectionArgumentType }, selectOriginProperty, selectLambdaExpression);
                    var memberAssignment2 = Expression.Bind(newPropertyInfo, memberAssignment);
                    binding.Add(memberAssignment2);
                }
                else if (TypesHelper.TypeIsComplex(property.OriginalType))
                {
                    var newPropertyInfo = newType.GetProperty(property.RequestedName);
                    var memberAssignment = Expression.Bind(newPropertyInfo, GenerateNewBody(property, Expression.Property(source, property.Name), newPropertyInfo.PropertyType, level++));

                    binding.Add(memberAssignment);
                }
                else
                {
                    var memberAssignment = Expression.Bind(newType.GetProperty(property.Name), Expression.Property(source, property.Name));

                    binding.Add(memberAssignment);
                }
            }

            var newExp = Expression.New(newType);
			var defaultExp = Expression.Default(newType);
			var body = Expression.MemberInit(newExp, binding);
            var expIf = Expression.Condition(Expression.NotEqual(source, Expression.Constant(null)), body, defaultExp);
            return expIf;
        }

        /// <summary>
        /// Method that gets a property name for a given lambda expression
        /// </summary>
        /// <typeparam name="TModel">Expression type attribute</typeparam>
        /// <param name="property">Property lambda expression</param>
        /// <returns>Name of the property</returns>
        internal static string GetPropertyName<TModel>(
            Expression<Func<TModel, object>> property
        ) where TModel : class
        {
            LambdaExpression lambda = (LambdaExpression)property;

            return GetPropertyName(lambda);
        }

        private static string GetPropertyName(LambdaExpression lambda)
        {
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)(lambda.Body);
                var memberExpression = (MemberExpression)(unaryExpression.Operand);

                return ((PropertyInfo)memberExpression.Member).Name;
            }
            else if (lambda.Body is MethodCallExpression)
            {
                var methodCallExpression = (MethodCallExpression)lambda.Body;

                return string.Join(".", ((PropertyInfo)((MemberExpression)methodCallExpression.Arguments[0]).Member).Name, GetPropertyName((LambdaExpression)methodCallExpression.Arguments[1]));
            }
            else
            {
                var memberExpression = (MemberExpression)(lambda.Body);

                return ((PropertyInfo)memberExpression.Member).Name;
            }
        }
        /// <summary>
        /// Method that generates a lambda expression for property name
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="type">type of the property</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Lambda expression for the property</returns>
        internal static Expression GenereteLambdaExpression<T>(
            ref Type type,
            string propertyName
        )
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
