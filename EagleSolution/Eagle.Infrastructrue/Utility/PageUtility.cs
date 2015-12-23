using System;
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
            var _pageCount = t.Count();
            var skipCount = 0;
            if (pageNumber > 0)
            {
                skipCount = (pageNumber - 1) * pageSize;
            }
            if (skipCount > _pageCount)
            {
                return new List<T>();
            }
            pageCount = (int)Math.Ceiling((decimal)_pageCount / pageSize);
            return t.Skip(skipCount).Take(pageSize);
        }
        public static IEnumerable<T> Pageing<T>(this IQueryable<T> t, int pageNumber, ref int pageCount)
        {
            if (t == null)
            {
                return new List<T>();
            }
            pageCount = t.Count();
            var skipCount = 0;
            if (pageNumber > 0)
            {
                skipCount = (pageNumber - 1);
            }
            if (pageNumber > pageCount)
            {
                return new List<T>();
            }
            return t.Skip(skipCount).Take(1);
        }
    }
}
