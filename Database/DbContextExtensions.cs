using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Database
{
    public static class DbContextExtensions
    {
        public static IQueryable<T> GetQuery<T>(this DatabaseContext context)
        {
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
            method = method.MakeGenericMethod(typeof(T));
            return method.Invoke(context, null) as IQueryable<T>;
        }
    }
}
