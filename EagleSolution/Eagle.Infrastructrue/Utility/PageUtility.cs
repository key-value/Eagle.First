using System.Collections.Generic;
using System.Linq;

namespace Eagle.Infrastructrue.Utility
{
    public static class PageUtility
    {
        public static IEnumerable<T> Pageing<T>(this IQueryable<T> t, int pageNumber, int pageSize, ref int pageCount)
        {
            if (t == null || pageSize == 0)
            {
                return new List<T>();
            }
            pageCount = (t.Count() + pageSize - 1) / pageSize;
            var skipCount = 0;
            if (pageNumber > 0)
            {
                skipCount = (pageNumber - 1) * pageSize;
            }
            if (pageNumber > pageCount)
            {
                return new List<T>();
            }
            return t.Skip(skipCount).Take(pageSize);
        }
    }
}
