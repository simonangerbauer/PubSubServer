using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Database
{
    /// <summary>
    /// Db context extensions.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Gets a generic query on the database context.
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="context">Context.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IQueryable<T> GetQuery<T>(this DatabaseContext context)
        {
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
            method = method.MakeGenericMethod(typeof(T));
            return method.Invoke(context, null) as IQueryable<T>;
        }
    }
}
