using NSI.Common.Models;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NSI.Common.Helpers
{
    public static class SortingHelper
    {
        public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            List<string> properties = property.Split('.').ToList();
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (string prop in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(prop);

                if (propertyInfo == null)
                {
                    throw new ArgumentException(ExceptionMessages.PropertyDoesNotExist);
                }

                expr = Expression.Property(expr, propertyInfo);
                type = propertyInfo.PropertyType;
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        }

        public static IOrderedQueryable<T> ApplySortRule<T>(IOrderedQueryable<T> data, SortCriteria rule, Func<string, Expression<Func<T, object>>> sortFunctor)
        {
            if (rule == null || string.IsNullOrWhiteSpace(rule.ColumnName))
            {
                return data;
            }

            Expression<Func<T, object>> fnc = sortFunctor(rule.ColumnName);

            if (fnc == null)
            {
                throw new ArgumentException("fnc");
            }

            switch (rule.Order)
            {
                case System.Data.SqlClient.SortOrder.Descending:
                    return data.ThenByDescending(fnc);
                case System.Data.SqlClient.SortOrder.Ascending:
                default:
                    return data.ThenBy(fnc);
            }
        }
    }
}
