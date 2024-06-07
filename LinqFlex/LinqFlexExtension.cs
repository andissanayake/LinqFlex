using System.Linq.Expressions;

namespace LinqFlex
{
    public static class LinqFlexExtension
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity, TKey>(this IQueryable<TEntity> source, Expression<Func<TEntity, TKey>> keySelector, bool isAscending)
        {
            var query = isAscending
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);

            return query;
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName, bool isAscending = true)
        {
            var entityType = typeof(TEntity);
            var property = entityType.GetProperties()
                                     .FirstOrDefault(prop => string.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{entityType.Name}'.");
            }

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.Property(parameter, property);

            // Ensure we handle value types without boxing
            LambdaExpression keySelector;
            if (property.PropertyType.IsValueType)
            {
                var type = typeof(Func<,>).MakeGenericType(entityType, property.PropertyType);
                keySelector = Expression.Lambda(type, propertyAccess, parameter);
            }
            else
            {
                var type = typeof(Func<,>).MakeGenericType(entityType, typeof(object));
                keySelector = Expression.Lambda(type, Expression.Convert(propertyAccess, typeof(object)), parameter);
            }

            var query = isAscending
                ? source.Provider.CreateQuery<TEntity>(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderBy",
                        new Type[] { entityType, keySelector.ReturnType },
                        source.Expression,
                        Expression.Quote(keySelector)))
                : source.Provider.CreateQuery<TEntity>(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderByDescending",
                        new Type[] { entityType, keySelector.ReturnType },
                        source.Expression,
                        Expression.Quote(keySelector)));

            return (IOrderedQueryable<TEntity>)query;
        }
    }
}
