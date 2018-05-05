using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostControl.Helpers
{
    public static class Helpers
    {
        public static IEnumerable<T> ForEachCustom<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var cur in enumerable)
            {
                action(cur);
            }
            return enumerable;
        }
    }
}
