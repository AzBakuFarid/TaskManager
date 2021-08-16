using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaskManager.EfCore.Extensions
{
    public static class IncludeExtension
    {
        public static IQueryable<TDbEntity> AddInclude<TDbEntity>(this IQueryable<TDbEntity> query, params string[] includes) where TDbEntity : class
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var inclue in includes)
                {
                    query = query.Include(inclue);
                }
            }

            return query;
        }
    }
}
