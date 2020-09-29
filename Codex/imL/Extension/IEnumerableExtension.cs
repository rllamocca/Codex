using System.Collections.Generic;
using System.Linq;

namespace Codex.Extension
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> DefaultOrEmpty<T>(this IEnumerable<T> _a)
        {
            return _a ?? Enumerable.Empty<T>();
        }
    }
}
