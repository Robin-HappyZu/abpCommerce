using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Common
{
    public static class QueryPredicateHelper
    {
        public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderedQuery<T>(QueryableOrderBy[] orderbys, Expression[] predicate)
        {
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            MethodCallExpression resultExp = null;
            foreach (var item in predicate)
            {
                resultExp = Expression.Call(typeof(Queryable), "Where", new[] { typeof(T) }, resultExp ?? outerExpression.Body, Expression.Quote(item));
            }
            foreach (var item in orderbys)
            {
                string[] props = item.OrderColumn.Split('.');
                IQueryable<T> query = new List<T>().AsQueryable<T>();
                Type type = typeof(T);
                ParameterExpression arg = Expression.Parameter(type, "x");

                Expression expr = arg;
                var types = new List<Type>
                {
                    typeof(T)
                };
                foreach (string prop in props)
                {
                    PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    expr = Expression.Property(expr, pi);
                    types.Add(pi.PropertyType);
                }
                LambdaExpression lambda = Expression.Lambda(expr, arg);
                string methodName = item.OrderType.ToString();

                resultExp = Expression.Call(typeof(Queryable), methodName, types.ToArray(), resultExp ?? outerExpression.Body, Expression.Quote(lambda));
            }
            if (resultExp == null)
            {
                return null;
            }

            var finalLambda = Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }
    }
}
