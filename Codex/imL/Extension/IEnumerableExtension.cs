using System.Collections.Generic;
using System.Linq;

namespace Codex.Extension
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> DefaultOrEmpty<T>(this IEnumerable<T> _array)
        {
            return _array ?? Enumerable.Empty<T>();
        }
        public static bool HasValue<T>(this IEnumerable<T> _array)
        {
            return (_array != null);
        }
        public static bool HasValueCount<T>(this IEnumerable<T> _array)
        {
            return (_array.HasValue<T>() && _array.Count() > 0);
        }
    }
}
