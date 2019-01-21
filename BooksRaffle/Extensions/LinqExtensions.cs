using System;
using System.Collections.Generic;
using System.Linq;

namespace BookmarksRaffle.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> list, Func<T, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(item => item.First());
        }
    }
}