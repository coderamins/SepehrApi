using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Context
{
    public static class GlobalQueryFilters
    {
        public static IQueryable<T> ApplyGlobalFilters<T>(this IQueryable<T> query) where T : class, IAuditableBaseEntity<T>
        {
            return query.Where(entity => entity.IsActive);
        }

    }
}
