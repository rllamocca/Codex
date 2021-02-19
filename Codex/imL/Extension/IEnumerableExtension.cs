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
    }
}
