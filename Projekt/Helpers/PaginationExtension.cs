using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Helpers
{
    public static class PaginationExtensions
    {
        public static PagedList<T> AsPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        where T : class
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
