using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ListExtension
    {
        public static IEnumerable<object> AddRange(this IEnumerable<object> list, IEnumerable<object> items)
        {
            var temList = list.ToList();
            temList.AddRange(items);

            return temList;
        }

        public static IEnumerable<object> Add(this IEnumerable<object> list, object item)
        {
            var temList = list.ToList();
            temList.Add(item);

            return temList;
        }
    }
}
